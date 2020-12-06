import socket
import enum

######## Interface ########
class EMG(enum.Enum):
  RELAX = 0
  FLEXION = 1
  EXTENSION = 2
  PRONATION = 3
  SUPINATION = 4

  def __int__(self):
    return self.value

  def __str__(self):
    return str(self.value)

def sendData(data):
  # print("Sending Data " + data + " to " + str(address))
  bytesToSend = str.encode(data)
  UDPServerSocket.sendto(bytesToSend, address)

###### Initialization ######
UDP_IP_ADDRESS = "127.0.0.1"
UDP_PORT = 18500
UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
address = ('127.0.0.1', 18500)
print("(streamer.py) Started UDP Streamer");
########### Test ###########
# test sending numbers 1 - 10000
def testSend():
  for i in range(10000):
    sendData(str(i))

# test sending all inputs incrementally, FLEX, RELAX, EXTEND, RELAX ( loop )
def testStream():
  i = 0
  while(True):
    if(0<= i < 5000):
      sendData(str(EMG.RELAX))
    elif (5000 <= i < 10000):
      sendData(str(EMG.FLEXION))
    elif (10000 <= i < 15000):
      sendData(str(EMG.RELAX))
    elif (15000 <= i < 20000):
      sendData(str(EMG.EXTENSION))
    elif (25000 <= i < 30000):
      sendData(str(EMG.RELAX))
    elif (35000 <= i < 40000):
      sendData(str(EMG.PRONATION))
    elif (45000 <= i < 50000):
      sendData(str(EMG.RELAX))
    elif (55000 <= i < 60000):
      sendData(str(EMG.SUPINATION))

    if(60000 < i):
      i = 0
    i+=1




############################################################################################################################################
## 9/29/2020 - Note on not binding on server.py / Note on binding on client.py
#
# The classical UDP server client model suggests having the server bind to a "well-known" port address, 
# and sends data to client only after client first makes requests.
# 
# Helpful Image of Classical UDP Diagram
# https://stackoverflow.com/questions/23068905/is-bind-necessary-if-i-want-to-receive-data-from-a-server-using-udp
# 
# Classical UDP Server Client model 
# <Server Side>
# 1. data, address = UDPServerSocket.recvfrom(BUFFER_SIZE)

# and then send packet to 
# 2. UDPServerSocket.sendto(bytesToSend, address)

# This means that you can't send any data until you get the address of the UDP Client. 
# This is inefficient because every time a server or client is disconnected, the entire 
# connection process needs to be done again.


# This probably means that server-client model be swapped around, so Signal Processor/ ML side is now the client and Unity is the server.
# New UDP Client: DSP/ML Classifier
# New UDP Server: Unity Receiver
# 1. Client simply broadcast/streams to a specified address and port
# 2. Server - who is binded to the specified address and port - receives data from Client
# 3. Server doesn't send anything back
