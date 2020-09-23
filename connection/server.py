import socket

###### Interface ######
def connect():
  global address
  print("UDP Server up and listening" )
  print("Waiting for request")
  data, address = UDPServerSocket.recvfrom(BUFFER_SIZE)

  # data, address = UDPServerSocket.recvfrom(BUFFER_SIZE)
  print("Data from Client: {}".format(data))
  print("Client IP Address: {}".format(address))

def sendData(data):
  bytesToSend = str.encode(data)
  UDPServerSocket.sendto(bytesToSend, address)

###### Initialization ######
UDP_IP_ADDRESS = "127.0.0.1"
UDP_PORT = 18500
BUFFER_SIZE = 1024
UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
UDPServerSocket.bind((UDP_IP_ADDRESS, UDP_PORT))
address = ""
connect()

##################
###### Test ######
##################
# test sending numbers 1 - 10000
def testSend():
  for i in range(10000):
    sendData(str(i))

# test sending all inputs incrementally, FLEX, RELAX, EXTEND, RELAX ( loop )
def testStream():
  RELAX = 0
  FLEXION = 1
  EXTENSION = 2

  i = 0
  while(True):

    if(i < 1000):
      sendData(str(FLEXION))
    elif (5000 <= i < 10000):
      sendData(str(EXTENSION))
    else:
      sendData(str(RELAX))
    
    if(20000 < i):
      i = 0
    i+=1

