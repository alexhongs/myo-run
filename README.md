# myo-run

Myoelectric Runner Game controlled with Muscle

- Add .gitignore separately in individual projects

### Muscle Classifier
Digital Signal Processing and Machine Learning
TODO: need comments and code for muscle classification here


### Unity Game Project
List of Projects:
- simpleBallGame
- myo-run

Opening Unity Game Project
1. Download Unity Hub from (https://unity3d.com/get-unity/download)
2. Open Unity Hub
3. Click Projects > Add > go into myo-run directory > click Unity Project and click Open
4. Open Project
5. Under Projects Tab, go to Assets > Scenes > and click Scenes to load project
6. Click Play button to start game


### UDP Connection
UDP Streamer - sends data to specific address port. This is the Client in classical UDP Server Client model.
UDP Receiver continuously receives information from the specific address port, having been binded to the port. This is the Server in classical UDP Server model.

Check out connection/README.md for more explanation.


### Mock Muscle Classifier

Mock Muscle Classifier simulates results we expect to get from DSP/ML Muscle Classifier. Use mockController.py to mock sending inputs from steamer using keyboard inputs (left, right, up, down arrows).
Left: Flexion, Right: Extension, Up: Pronation, Down: Supination

```
pip install pygame
python connection/mockController.py
```