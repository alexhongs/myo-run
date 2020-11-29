import streamer
from streamer import EMG
import serial
import time
import matplotlib.pyplot as plt
import sys
import string
import numpy as np
from sklearn.svm import SVC

def trainSVM():

    close=np.load("close_fist_feature_vector.npy")
    extension=np.load("wrist_extension_feature_vector.npy")
    flextaion=np.load("wrist_flexion_feature_vector.npy")
    pronation=np.load("pronation_feature_vector.npy")
    supination=np.load("supination_feature_vector.npy")

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

    clf = SVC()
    clf.fit(data,labels)
    SVC()

    return clf


def main():

    clf=trainSVM()
    numElectrodes=5

    # set up the serial line
    ser = serial.Serial('/dev/cu.usbmodem143101', 9600)

    while True:


        #make it rolling at some point
        window=[0]*300;
        for i in range(0,300):
            b=ser.readline()         # read a byte string
            string_n=b.decode()      # decode byte string into Unicode  
            string=string_n.rstrip() # remove \n and \r

            newSample=[0]*numElectrodes;

            j=0;
            for val in string.split("  "):
                newSample[j]=int(val)
                j+=1
            window.append(newSample)

        print(window)
        #extract feartures from window
        classification=clf.predict([window])


        if(classification==1):
            print("LEFT")
            streamer.sendData(str(EMG.EXTENSION))
            #time.sleep(0.001) 
        elif(classification==2 or classification==0):
            print("RIGHT")
            streamer.sendData(str(EMG.FLEXION))
        elif(classification==3):
            print("UP")
            streamer.sendData(str(EMG.PRONATION))
        elif(classification==4):
            print("DOWN")
            streamer.sendData(str(EMG.SUPINATION))
        else:
            print("relax")
            streamer.sendData(str(EMG.RELAX))
            #time.sleep(0.001) 

main()