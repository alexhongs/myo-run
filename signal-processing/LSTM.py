##### LSTM Model #######
import numpy as np
import sys
import matplotlib.pyplot as plt
import string
import tensorflow

from tensorflow import keras
from keras.preprocessing import sequence
from keras.models import Sequential
from keras.layers import Dense
from keras.layers import LSTM

from keras.optimizers import Adam
from keras.models import load_model
from keras.callbacks import ModelCheckpoint


close_fist = np.load("close_fist_trials.npy")
wrist_flexion = np.load("wrist_flexion_trials.npy")
wrist_extension = np.load("wrist_extension_trials.npy")
pronation = np.load("pronation_trials.npy")
supination = np.load("supination_trials.npy")

n = 42 # number of samples
t = 300 # number of time stamps
e = 5 # number of electrodes

# close_fist = [0:3, 4:7, 8:10]
# wrist_flexion = [0:1, 2:3, 4:5]
# wrist_extension = [0:2, 3:5, 6:8]
# pronation = [0, 1, 2:3]
# supination = [0:3, 4:7, 8:11]

train = np.zeros([n//3,t,e])
train[0:3,:,:] = (close_fist[0:3])
train[4:5,:,:] = (wrist_flexion[0:1])
train[6:8,:,:] = (wrist_extension[0:2])
train[9,:,:] = (pronation[0])
train[10:13,:,:] = (supination[0:3])
print("train")
print(train.shape)

validation = np.zeros([n//3,t,e])
validation[0,:,:] = (pronation[1])
validation[1:4,:,:] = (supination[4:7])
validation[5:8,:,:] = (close_fist[4:7])
validation[9:11,:,:] = (wrist_extension[3:5])
validation[12:13,:,:] = (wrist_flexion[2:3])
print("validation")
print(validation.shape)

test = np.zeros([n//3,t,e])
test[0:2,:,:] = (close_fist[8:10])
test[3:4,:,:] = (pronation[2:3])
test[5:6,:,:] = (wrist_flexion[4:5])
test[7:10,:,:] = (supination[8:11])
test[11:13,:,:] = (wrist_extension[6:8])
print("test")
print(test.shape)

# train_target = ['c','c','c','c','f','f','e','e','e','p','s','s','s','s']
# validation_target = ['p','s','s','s','s','c','c','c','c','e','e','e','f','f']
# test_target = ['c','c','c','p','p','f','f','s','s','s','s','e','e','e']
train_target = [0,0,0,0,1,1,2,2,2,3,4,4,4,4]
validation_target = [3,4,4,4,4,0,0,0,0,2,2,2,1,1]
test_target = [0,0,0,3,3,1,1,4,4,4,4,2,2,2]

train_target = np.array(train_target)
validation_target = np.array(validation_target)
test_target = np.array(test_target)


#### Building the Model #######

model = Sequential()
model.add(LSTM(256, input_shape=(300, 5)))
model.add(Dense(1, activation='sigmoid'))

model.summary()

adam = Adam(lr=0.001)
chk = ModelCheckpoint('best_model.pkl', monitor='val_acc', save_best_only=True, mode='max', verbose=1)
model.compile(loss='binary_crossentropy', optimizer=adam, metrics=['accuracy'])
model.fit(train, train_target, epochs=200, batch_size=128, callbacks=[chk], validation_data=(validation,validation_target))

# #loading the model and checking accuracy on the test data
# model = load_model('best_model.pkl')

from sklearn.metrics import accuracy_score
test_preds = np.argmax(model.predict(test), axis=-1)
# test_preds = model.predict_classes(test)
print(accuracy_score(test_target, test_preds))
