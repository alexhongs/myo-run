import numpy as np
import sys
import matplotlib.pyplot as plt
import string

dataFile = open(sys.argv[1],'r').read()
dataLabels = open(sys.argv[2],'r').read()
numElectrodes=5

#make empty list to hold data + labels of data
data=[]
labels=[]


for label in dataLabels.splitlines():
    labels.append(label)
#labels is a vector of length 42 that matches with each trial


#each line is a new trial
for trial in dataFile.splitlines():
    newTrial=[]

    #each trial has 300 unique time samples
    for timeSample in trial.split(';'):
        newTime=[]

        #there are five electrodes per time sample
        for electrode in timeSample.split(','):
            electrode=electrode.replace('[','')
            electrode=electrode.replace(']','')
            electrode=electrode.strip()
            if electrode=='':
                continue
            newTime.append(int(electrode))
        if newTime!=[]:
            newTrial.append(newTime)

    data.append(newTrial)

#data should now be 42x300x5 (there are 42 trials rn)

npData=np.array(data)

#uncomment these if you wanna see what it looks like
#print(npData)
#print(npData.shape)



