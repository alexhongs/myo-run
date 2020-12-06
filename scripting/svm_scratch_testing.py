import numpy as np
from sklearn.svm import SVC
import time

t0=time.time()

close=np.load("close_fist_feature_vector.npy")
extension=np.load("wrist_extension_feature_vector.npy")
flextaion=np.load("wrist_flexion_feature_vector.npy")
pronation=np.load("pronation_feature_vector.npy")
supination=np.load("supination_feature_vector.npy")

t1=time.time()-t0
print(t1)


data=[]
labels=[]

for trial in close:
    data.append(trial.tolist())
    labels.append(0)

for trial in extension:
    data.append(trial.tolist())
    labels.append(1)

for trial in flextaion:
    data.append(trial.tolist())
    labels.append(2)

for trial in pronation:
    data.append(trial.tolist())
    labels.append(3)

for trial in supination:
    data.append(trial.tolist())
    labels.append(4)

#print(data)
#print(labels)


#labels 0:C 1:E 2:F 3:P 4:S


X = [[0, 0], [1, 1], [2,2]]
y = [1,2,3]
clf = SVC()
#clf.fit(X, y)
clf.fit(data,labels)
SVC()

#print(extension[2].tolist())


print(clf.predict([pronation[2].tolist()]))

#print(clf.predict([[2., 2.]]))