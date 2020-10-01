# UDP Connection

A UDP Connection socket for streaming data from Streamer to Receiver

## Components

Contains 2 main components and 2 test components
Main Components:
- Streamer (streamer.py)
- Receiver (receiver.py)

Test Components:
- Test Streamer (streamer.py)
- Mock Muscle Classifier (mockController.py)

### Streamer (UDPClient)  &   Receiver (UDPServer)

If the Streamer is the server, the server needs to wait for initial message from client.
This is unneccesssary and inefficient, because streamer does not need to receive and message,
it just needs to broadcast information to the receiver of information.
Reversing the originally intended UDP Client Server model,

The Streamer is the client, sending data to specific address port, 
The Receiver continuously receives information from the specific address port, having been binded to the port.


### Mock Muscle Classifier (uses Streamer)

Mock Muscle Classifier simulates results we expect to get from DSP/ML Muscle Classifier. Use mockController.py to mock sending inputs from steamer using keyboard inputs (left, right, up, down arrows).
Left: Flexion, Right: Extension, Up: Pronation, Down: Supination

```
pip install pygame

python mockController.py
```

## Model
### Reverse UDP Server Client Model

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
- mockController.py - sends mock inputs controlled by keyboard

New UDP Server
- Rising and Falling edge detection


## Testing

### 1. Open Receiver
You can open up a Unity Project or testUDPConnection, or run
```
python receiver.py
```

### 2. Open Test Streamer (Option 1)
```
python
exec(open("streamer.py").read())
testStream()
```

### 2. Open Mock Controller (Option 2)
```
pip install pygame

python mockController.py
```
