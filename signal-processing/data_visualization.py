import numpy as np
import sys
import matplotlib.pyplot as plt
import string

outfile_data = "preliminary_data.npy"
outfile_labels = "preliminary_data_labels.npy"

npData = np.load(outfile_data)
labels = np.load(outfile_labels)

n = npData[:,1,1].size # number of samples
t = npData[1,:,1].size # number of time stamps
e = npData[1,1,:].size # number of electrodes

max_data = np.zeros((n, e))

for i in range(n):
    max_data_entry = [0, 0, 0, 0, 0]
    for j in range(e):
        max_data_entry[j] = max(npData[i,:,j])
    max_data[i,:] = max_data_entry


close_fist = []
wrist_flexion = []
wrist_extension = []
pronation = []
supination = []

close_fist_max = []
wrist_flexion_max = []
wrist_extension_max = []
pronation_max = []
supination_max = []

for l in range(n):
    value = max_data[l,:]
    if labels[l] == 'c':
        close_fist_max.append(value)
        close_fist.append(npData[l,:,:])
    elif labels[l] == 'f':
        wrist_flexion_max.append(value)
        wrist_flexion.append(npData[l,:,:])
    elif labels[l] == 'e':
        wrist_extension_max.append(value)
        wrist_extension.append(npData[l,:,:])
    elif labels[l] == 'p':
        pronation_max.append(value)
        pronation.append(npData[l,:,:])
    elif labels[l] == 's':
        supination_max.append(value)
        supination.append(npData[l,:,:])

print("Close fist: " + str(len(close_fist_max)))
print("Flexion: " + str(len(wrist_flexion_max)))
print("Extension: " + str(len(wrist_extension_max)))
print("Pronation: " + str(len(pronation_max)))
print("Supination: " + str(len(supination_max)))

np.save("close_fist_trials", close_fist)
np.save("wrist_flexion_trials", wrist_flexion)
np.save("wrist_extension_trials", wrist_extension)
np.save("pronation_trials", pronation)
np.save("supination_trials", supination)

close_fist_max_avg = []
wrist_flexion_max_avg = []
wrist_extension_max_avg = []
pronation_max_avg = []
supination_max_avg = []

for i in range(e):
    close_fist_max_avg.append(np.mean([x[i] for x in close_fist_max]))
    wrist_extension_max_avg.append(np.mean([x[i] for x in wrist_extension_max]))
    wrist_flexion_max_avg.append(np.mean([x[i] for x in wrist_flexion_max]))
    pronation_max_avg.append(np.mean([x[i] for x in pronation_max]))
    supination_max_avg.append(np.mean([x[i] for x in supination_max]))


## Plots 

plt.figure(figsize=(10,5))
plt.subplot(2,3,1)
plt.plot(close_fist_max, 'x')
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Close Fist Max")

plt.subplot(2,3,2)
plt.plot(wrist_flexion_max, 'x')
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Wrist Flexion Max")

plt.subplot(2,3,3)
plt.plot(wrist_extension_max, 'x')
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Wrist Extension Max")

plt.subplot(2,3,4)
plt.plot(pronation_max, 'x')
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Pronation Max")

plt.subplot(2,3,5)
plt.plot(supination_max, 'x')
plt.title("Supination Max")
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')

plt.suptitle('Electrode Max for Each Movement', fontsize=16)
plt.figlegend(['E1', 'E2', 'E3', 'E4', 'E5'])
plt.subplots_adjust(wspace=0.5)
plt.subplots_adjust(hspace=0.5)
plt.show()


x = ['E1', 'E2', 'E3', 'E4', 'E5']
plt.figure(figsize=(10,5))
plt.subplot(2,3,1)
plt.bar(x, close_fist_max_avg)
plt.title("Close Fist Electrode Max Average")
plt.tick_params(labelsize=8)  

plt.subplot(2,3,2)
plt.bar(x, wrist_flexion_max_avg)
plt.title("Wrist Flexion Electrode Max Average")

plt.subplot(2,3,3)
plt.bar(x, wrist_extension_max_avg)
plt.title("Wrist Extension Electrode Max Average")

plt.subplot(2,3,4)
plt.bar(x, pronation_max_avg)
plt.title("Pronation Electrode Max Average")

plt.subplot(2,3,5)
plt.bar(x, supination_max_avg)
plt.title("Supination Electrode Max Average")

plt.subplots_adjust(hspace=0.5)
plt.subplots_adjust(wspace=1.5)
plt.suptitle('Average Electrode Max for Each Movement', fontsize=16)
plt.show()
