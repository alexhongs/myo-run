import streamer
from streamer import EMG
import serial
import time
import matplotlib.pyplot as plt
import sys
import string

#import pygame

##############################
# Mock EMG Muscle Controller
# Simulates Muscle Classification Results with Keyboard Inputs and
# sends results over to Receiver through UDP
# Flexion    : Left Arrow Key
# Extension  : Right Arrow Key
# Pronation  : Up Arrow Key
# Supination : Down Arrow Key
#############################
def main():

    # set up the serial line
    ser = serial.Serial('/dev/cu.usbmodem143101', 9600)



    while True:

        b = ser.readline()         # read a byte string
        string_n = b.decode()      # decode byte string into Unicode  
        string = string_n.rstrip() # remove \n and \r
        #print(string)
        value=int(string)
        goingLeft=value>200
        print(value)
        #time.sleep(.001)
        

        if(goingLeft):
            print("LEFT")
            streamer.sendData(str(EMG.FLEXION))
            time.sleep(1) 
        else:
            print("relax")
            streamer.sendData(str(EMG.RELAX))
            time.sleep(0.001) 

main()