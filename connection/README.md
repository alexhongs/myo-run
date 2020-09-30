# Test UDP Connection

A test UDP Connection script for streaming data from client to server

## Reverse UDP Server Client Model
### Streamer (UDPClient)  &   Receiver (UDPServer)

If the Streamer is the server, the server needs to wait for initial message from client.
This is unneccesssary and inefficient, because streamer does not need to receive and message,
it just needs to broadcast information to the receiver of information.
Reversing the originally intended UDP Client Server model,

The Streamer is the client, sending data to specific address port, 
The Receiver continuously receives information from the specific address port, having been binded to the port.


### Integration with Myo-run
Before:
- UDP Server: Muscle Classifier
- UDP Client: Unity

After:
- UDP Server: Unity
- UDP Client: Muscle Classifier

1. New Client simply broadcast/streams to a specified address and port
2. New Server - who is binded to the specified address and port - receives data from Client
3. New Server doesn't send anything back


New UDP Client
- testStream() - sends mock inputs 0000 1111 0000 2222 ....

New UDP Server
- Rising and Falling edge detection

Usage

```
// On Shell 1
python3
exec(open("streamer.py").read())

// On Shell 2
python receiver.py

// On Shell 1
testStream()
```
