import numpy as np
from scipy import signal
import matplotlib.pyplot as plt

outfile_data = "preliminary_data.npy"
npData = np.load(outfile_data)

#### Butterworth Filter Data #####
fs = 1000
cutoff = np.array([50, 150]) * 2 / fs
b, a = signal.butter(5, cutoff, btype='bandpass')

n = 42 # number of samples
t = 300 # number of time stamps
e = 5 # number of electrodes
filtered = np.zeros([n,t,e])

for i in range(n):
    for j in range(e):
      filtered[i,:,j] = np.array(signal.filtfilt(b, a, npData[i,:,j]))

plt.figure(figsize=(11,5))
plt.suptitle('Original and Filtered Data for Sample 1', fontsize=16)

plt.subplot(1,5,1)
plt.plot(npData[1,:,0])
plt.plot(filtered[1,:,0])
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Electrode 1")

plt.subplot(1,5,2)
plt.plot(npData[1,:,1])
plt.plot(filtered[1,:,1])
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Electrode 2")

plt.subplot(1,5,3)
plt.plot(npData[1,:,2])
plt.plot(filtered[1,:,2])
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Electrode 3")

plt.subplot(1,5,4)
plt.plot(npData[1,:,3])
plt.plot(filtered[1,:,3])
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Electrode 4")

plt.subplot(1,5,5)
plt.plot(npData[1,:,4])
plt.plot(filtered[1,:,4])
plt.xlabel('Sample')
plt.ylabel('Voltage (mV)')
plt.title("Electrode 5")

plt.subplots_adjust(wspace=1.5)
plt.figlegend(['Original', 'Filtered'])
plt.show()

# outfile_data = "preliminary_data_filtered"
# np.save(outfile_data, filtered)
