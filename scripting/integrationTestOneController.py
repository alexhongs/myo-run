import streamer
from streamer import EMG
import serial
import time
import matplotlib.pyplot as plt
import sys
import string

import pygame

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

    pygame.init()
    screen = pygame.display.set_mode((640, 480))

    while True:
        pressed = pygame.key.get_pressed()
        if(pressed[pygame.K_LEFT]):
            print("FLEX")
            streamer.sendData(str(EMG.FLEXION))
        elif(pressed[pygame.K_RIGHT]):
            print("EXTENSION")
            streamer.sendData(str(EMG.EXTENSION))
        elif(pressed[pygame.K_UP]):
            print("PRONATION")
            streamer.sendData(str(EMG.PRONATION))
        elif(pressed[pygame.K_DOWN]):
            print("SUPINATION")
            streamer.sendData(str(EMG.SUPINATION))
        else:
            print("r")
            streamer.sendData(str(EMG.RELAX))

        # Need this segment here to avoid over-looping the while loop
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                sys.exit()

main()