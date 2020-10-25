import serial
import time
import matplotlib.pyplot as plt
import sys

outputFile = open(sys.argv[1],'w')
outputLabels = open(sys.argv[2],'w')

# set up the serial line
ser = serial.Serial('/dev/cu.usbmodem143101', 9600)

for j in range(20): #how many trials
    print("entering trial",j)
    time.sleep(1.5)
    #print('recording now')
    #time.sleep(1)
    # Read and record the data
    data =[]                       # empty list to store the data
    for i in range(300):           #how many ms to sample for
        b = ser.readline()         # read a byte string
        string_n = b.decode()      # decode byte string into Unicode  
        string = string_n.rstrip() # remove \n and \r
        #print(string)
        flt = float(string)        # convert string to float
        print(flt)
        data.append(flt)           # add to the end of data list
        time.sleep(0.001)            # nyquist is 1000 Hz

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






