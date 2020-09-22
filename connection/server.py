import socket

UDP_IP_ADDRESS = "127.0.0.1"
UDP_PORT = 18500
BUFFER_SIZE = 1024

UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

UDPServerSocket.bind((UDP_IP_ADDRESS, UDP_PORT))

print("UDP Server up and listening")

print "Waiting for request"
data, address = UDPServerSocket.recvfrom(BUFFER_SIZE)
print "Data from Client: {}".format(data)
print "Client IP Address: {}".format(address)

# Stream in data to client as soon as the connection is open
count = 0
while (True):
  msgFromServer       = "Hello UDP Client | packet number: " + str(count)
  bytesToSend         = str.encode(msgFromServer)

  UDPServerSocket.sendto(bytesToSend, address)
  count += 1


# TODO: Need to stream in real-time data. Perhaps a singler liner function sendData() that just sends to client might suffice
# TODO: Need to mock real data and send that. We may also be able to get this in interpreter mode where sendData() is called to
# test control game could work
def sendData(data):
  bytesToSend = str.encode(data)
  UDPServerSocket.sendto(bytesToSend, address)