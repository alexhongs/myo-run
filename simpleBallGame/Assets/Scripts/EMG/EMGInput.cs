using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;

public enum EMG : byte
{
    None = 0,
    Flexion = 1,
    Extension = 2,
    Supination = 3,
    Pronation = 4,
}

public class EMGInput : MonoBehaviour
{
    EMG lastInput;

    Socket socket;
    Thread thread;
    void Start()
    {
        lastInput = EMG.None;

        thread = new Thread(StartReceiver);
        thread.Priority = System.Threading.ThreadPriority.BelowNormal;
        thread.Start();
    }

    void StartReceiver()
    {
        Debug.Log("Starting Client");

        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18500);
        EndPoint ep = (EndPoint)ipep;
        socket = new Socket(AddressFamily.InterNetwork,
                  SocketType.Dgram, ProtocolType.Udp);
        socket.Bind((EndPoint)ipep);

        while (true)
        {
            byte[] data = new byte[1024];
            int recv = socket.ReceiveFrom(data, ref ep);
            //Debug.Log(Encoding.ASCII.GetString(data, 0, 1));

            string b = Encoding.ASCII.GetString(data, 0, 10); // Received
            if (b == "0")
            {
                //Debug.Log("Received " + b);
            }
            else if (b == "1")
            {
                //Debug.Log("Received " + b);
            }
            else
            {
                //Debug.Log("Received " + b);
            }

            DecodeMessage(b);
        }
    }

    private void DecodeMessage(string message)
    {
        try
        {
            EMG newInput = (EMG)Byte.Parse(message);
            if (lastInput.Equals(EMG.None) && !lastInput.Equals(newInput))
            {
                Debug.Log("Rising Edge" + newInput);
                //controller.Do(newInput);
                lastInput = newInput;
            }
            else if (newInput.Equals(EMG.None) && !lastInput.Equals(newInput)) // This is assuming it always reaches 0 before other input
            {
                //Debug.Log("Falling Edge" + newInput);
                lastInput = newInput;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    public bool getButtonInput(EMG emg)
    {
        return lastInput.Equals(emg);
    }

    public void OnDestroy()
    {
        socket.Close();
        thread.Abort();
    }

    public void OnApplicationQuit()
    {
        socket.Close();
        thread.Abort();
    }
}

