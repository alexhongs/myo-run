import streamer
from streamer import EMG
import serial
import time
import matplotlib.pyplot as plt
import sys
import string
import numpy as np
from sklearn.svm import SVC
import string
from scipy.signal import find_peaks
import statistics as stat
from scipy import signal
from scipy.fft import fft
from pywt import dwt
import queue
from threading import Thread


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

    print("trained SVM")

    return clf


def featureExtraction(window):
    
    t = 300 # number of time stamps
    e = 5 # number of electrodes

    #npData=np.zeros([t,e]) # replace npData here ######
    npData=np.array(window)

    #### Butterworth Filter Data #####
    fs = 1000
    cutoff = 150 * 2 / fs
    b, a = signal.butter(5, cutoff, btype='lowpass')

    filtered = np.zeros([t,e])

    for j in range(e):
      filtered[:,j] = signal.filtfilt(b, a, npData[:,j])

    ######## Max Data ########

    max_data = [0 for x in range(e)]
    for j in range(e):
        max_data[j] = max(npData[:,j])

    ##### FFT ######
    half = 150
    data_fft = np.zeros([half,e])

    for k in range(e):
      data_fft[:,k] = fft(npData[:,k])[0:half]

    feature_vector_size = 15

    ### Close Fist ####

    feature_vector = [0 for x in range(feature_vector_size)]

    mean = np.mean(max_data)
    for j in range(e):
      feature_vector[j] = max_data[j]
      if max_data[j] > mean:
          feature_vector[j+5] = 1

      peaks, _ = find_peaks(data_fft[:,j])
      if len(peaks) == 0:
        max_peak = -1
      else:
        max_peak = peaks[0]
        if peaks[0] == 0:
          max_peak = peaks[1]
      feature_vector[j+10] = max_peak

    print(feature_vector)

    return feature_vector


def recordWindow(que_ret,ser,clf):

    while True:
        print("entering sensing thread")

        numElectrodes=5

        #print("still here")

        window=[0]*300;
        for i in range(0,300):
            #print("in the reading loop now")
            b=ser.readline()         # read a byte string
            string_n=b.decode()      # decode byte string into Unicode  
            string=string_n.rstrip() # remove \n and \r

            newSample=[0]*numElectrodes;

            j=0;
            #print(string)
            for val in string.split("  "):
                newSample[j]=int(val)
                j+=1
            window[i]=newSample

        #print(window)
        #print("extracting some features")
        featureVector=featureExtraction(window)

        #print("we still out here")
        #extract features from window
        classification=clf.predict([featureVector])
        que_ret.put(classification)
        print("classification %d enqueued" %classification)

        #t.join()



def main():

    clf=trainSVM()
    que_ret = queue.Queue()

    # set up the serial line
    ser=serial.Serial('/dev/cu.usbmodem14301', 9600)
    motionDelay=100

    t=Thread(target=recordWindow, args=([que_ret,ser,clf]), daemon=False)
    t.start()

    while True:


        #print("queue size is ",que_ret.qsize())
        if que_ret.empty():
            classification=0
        else:
            classification=que_ret.get()

        #still need something to do with close_fist class

        if (classification==0): 
            #print("relax")
            streamer.sendData(str(EMG.RELAX))
        elif(classification==1):
            print("LEFT")
            for i in range(motionDelay):
                streamer.sendData(str(EMG.EXTENSION))
                time.sleep(0.001)
        elif(classification==2):
            print("RIGHT")
            for i in range(motionDelay):
                streamer.sendData(str(EMG.FLEXIION))
                time.sleep(0.001)
        elif(classification==3):
            print("UP")
            for i in range(motionDelay):
                streamer.sendData(str(EMG.PRONATION))
                time.sleep(0.001)
        elif(classification==4):
            print("DOWN")
            for i in range(motionDelay):
                streamer.sendData(str(EMG.SUPINATION))
                time.sleep(0.001)
        #no else 
        time.sleep(0.001) 

        #t.join()

main()