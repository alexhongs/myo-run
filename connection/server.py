import socket

UDP_IP_ADDRESS = "127.0.0.1"
UDP_PORT = 18500
BUFFER_SIZE = 1024

UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

UDPServerSocket.bind((UDP_IP_ADDRESS, UDP_PORT))

print("UDP Server up and listening")

while True:
    print "Waiting for request"
    data, address = UDPServerSocket.recvfrom(BUFFER_SIZE)
    print "Data from Client: {}".format(data)
    print "Client IP Address: {}".format(address)

    msgFromServer       = "Hello UDP Client"
    bytesToSend         = str.encode(msgFromServer)

    UDPServerSocket.sendto(bytesToSend, address)

