import streamer
from streamer import EMG

import pygame

#####
# Mock EMG Muscle Controller
# Flexion  : Left Arrow Key
# Extension: Right Arrow Key
# 
####
def main():
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
        else:
            print("r")
            streamer.sendData(str(EMG.RELAX))

        # Need this segment here to avoid over-looping the while loop
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                sys.exit()

main()