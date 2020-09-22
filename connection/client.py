import socket

UDP_ADDRESS_PORT   = ("127.0.0.1", 18500)
BUFFER_SIZE          = 1024

msgFromClient       = "Hello UDP Server"
bytesToSend         = str.encode(msgFromClient)

# Create a UDP socket at client side
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
UDPClientSocket.sendto(bytesToSend, UDP_ADDRESS_PORT)

# Loop listening for responses from udp server
while (True):  
  data, address = UDPClientSocket.recvfrom(BUFFER_SIZE)
  msg = "Server Response: {}".format(data)
  print(msg)