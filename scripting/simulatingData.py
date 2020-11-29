from read_csv_data import data,labels,npData
#import read_csv_data
import numpy as np
import sys
import matplotlib.pyplot as plt
import string

#trialNumber=14


# plt.plot(npData[trialNumber])
# trialLabel=labels[trialNumber]
# print(trialLabel)

# plt.xlabel('Time (milliseconds)')
# plt.ylabel('Sensor Readings')
# #plt.title('Sensor Readings vs. Time')
# plt.title('Trial number %d with label %s'%(trialNumber,trialLabel))


fig,axs=plt.subplots(7,2)

priorIndex=0
for i in range(7):
	for j in range(2):
		trialNumber=labels.index('e',priorIndex)
		priorIndex=trialNumber+1
		trialLabel=labels[trialNumber]
		axs[i,j].plot(npData[trialNumber])
		axs[i,j].set_title('Trial number %d with label %s'%(trialNumber,trialLabel))
		plt.xlabel('Time (milliseconds)')
		plt.ylabel('Sensor Readings')

plt.title('Extension Trials')
plt.show()


