import socket
RELAX = str(0)
FLEXION = str(1)
EXTENSION = str(2)
current = "0"

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
  if (current == RELAX and current != data): # rising edge
    print("Rising Edge" + msg)
    current = data

  elif (data == RELAX and current != data): # falling edge
    print("Falling Edge " + msg)
    current = data
  