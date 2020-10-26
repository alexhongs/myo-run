import serial
import time
import matplotlib.pyplot as plt
import sys
import string

outputFile = open(sys.argv[1],'w')
outputLabels = open(sys.argv[2],'w')
numElectrodes = 5;

# set up the serial line
ser = serial.Serial('/dev/cu.usbmodem143101', 9600)

for j in range(1): #how many trials
    print("entering trial",j)
    time.sleep(1.5)
    #print('recording now')
    #time.sleep(1)
    # Read and record the data
    #newTrial =[]   
    data=[]                    # empty list to store the data
    for i in range(300):           #how many ms to sample for
        b = ser.readline()         # read a byte string
        string_n = b.decode()      # decode byte string into Unicode  
        string = string_n.rstrip() # remove \n and \r
        #print(string)
        newSample=[0]*numElectrodes;
        i=0;
        for val in string.split("  "):
            #print("val is ",val)
            newSample[i]=int(val)
            i+=1
        #newTrial.append(newSample)
        data.append(newSample)
        time.sleep(0.001)            # nyquist is 1000 Hz
    #data.append(newTrial)


    # show the data

    for line in data:
        print(line)

    plt.plot(data)
    plt.xlabel('Time (milliseconds)')
    plt.ylabel('Sensor Reading')
    plt.title('Sensor Reading vs. Time')
    plt.show()

    val=input("Save: ")

    if (val=='y'):
        label=input("Motion? ")
        outputLabels.write(label)
        outputLabels.write("\n")
        
        for line in data:
            outputFile.write(str(line))
            outputFile.write(", ")

        outputFile.write("\n")



ser.close()






