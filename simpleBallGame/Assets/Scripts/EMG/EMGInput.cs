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
    UdpClient client;
    IPAddress serverIP;
    string hostIP = "127.0.0.1";
    int port = 18500;
    Socket socket;
    Byte[] buffer;
    EndPoint epServer;

    EMG lastInput;

    public EMGController controller;

    void Start()
    {
        lastInput = EMG.None;
        StartCoroutine(UDPClientReceive());
    }

    IEnumerator UDPClientReceive()
    {
        bool workDone = false;

        Debug.Log("Starting Client");
        serverIP = IPAddress.Parse(hostIP);
        IPEndPoint server = new IPEndPoint(serverIP, port);
        epServer = (EndPoint)server;


        client = new UdpClient();
        client.Connect(server);

        Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
        client.Send(sendBytes, sendBytes.Length);
        yield return null; // gives extra breathing time 
        //Inefficient algorithm but working version
        while (!workDone)
        {
            yield return null;
            Byte[] receiveBytes = client.Receive(ref server);
            string message = Encoding.ASCII.GetString(receiveBytes);
            DecodeMessage(message);
        }
    }

    private void ReceiveData(IAsyncResult ar)
    {
        try
        {
            // Receive all data
            int x = this.socket.EndReceive(ar);

            string message = Encoding.ASCII.GetString(buffer, 0, x);
            DecodeMessage(message);

            // Reset data stream
            this.buffer = new byte[1024];

            // Continue listening for broadcasts
            socket.BeginReceiveFrom(this.buffer, 0, this.buffer.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);
        }
        catch (ObjectDisposedException)
        { }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
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
                controller.Do(newInput);
                lastInput = newInput;
            }
            else if(newInput.Equals(EMG.None) && !lastInput.Equals(newInput))
            {
                Debug.Log("Falling Edge" + newInput);
                lastInput = newInput;
            }
                //if (current == RELAX and current != data): # rising edge
                //print("Rising Edge" + msg)
                //current = data

                //elif(data == RELAX and current != data): # falling edge
                //print("Falling Edge " + msg)
                //current = data

        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        

    }
}
