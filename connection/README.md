# Test UDP Connection

UDP Server
- testStream() - sends mock inputs 0000 1111 0000 2222 ....

UDP Client
- Rising and Falling edge detection

Usage

```
// On Shell 1
python3
exec(open("server.py").read())

// On Shell 2
python client.py

// On Shell 1
testStream()
```
