import socket

RELAX = b'0'
FLEXION = b'1'
EXTENSION = b'2'



UDP_ADDRESS_PORT   = ("127.0.0.1", 18500)
BUFFER_SIZE          = 1024

msgFromClient       = "Hello UDP Server"
bytesToSend         = str.encode(msgFromClient)

# Create a UDP socket at client side
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
UDPClientSocket.bind(UDP_ADDRESS_PORT)



current = "0"

# Loop listening for responses from udp server
while (True):  
  data, address = UDPClientSocket.recvfrom(BUFFER_SIZE)
  msg = "Receiving Msg from Client {}".format(data) + " | " + str(address)

  if (current == RELAX and current != data): # rising edge
    print("Rising Edge " + msg)
    current = data
  
  elif (data == RELAX and current != data): # falling edge
    print("Falling Edge " + msg)
    current = data
  

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