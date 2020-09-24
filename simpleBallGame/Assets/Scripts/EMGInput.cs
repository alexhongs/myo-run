using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class EMGInput : MonoBehaviour
{
    UdpClient client;
    IPAddress serverIP;
    string hostIP = "127.0.0.1";
    int port = 18500;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Debug.Log("Starting Client");
            //client = new UdpClient(port);
            //serverIP = IPAddress.Parse(hostIP);
            //IPEndPoint ipEnd = new IPEndPoint(serverIP, port);
            //client.Connect(ipEnd);
            //client.Client.Blocking = false;
            client = new UdpClient(port);
            client.Connect(hostIP, port);

            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            client.Send(sendBytes, sendBytes.Length);

            //IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Blocks until a message returns on this socket from a remote host.
            Byte[] receiveBytes = client.Receive(ref RemoteIpEndPoint);
            string returnData = Encoding.ASCII.GetString(receiveBytes);

            // Uses the IPEndPoint object to determine which of these two hosts responded.
            Debug.Log("This is the message you received " +
                                         returnData.ToString());
            Debug.Log("This message was sent from " +
                                        RemoteIpEndPoint.Address.ToString() +
                                        " on their port number " +
                                        RemoteIpEndPoint.Port.ToString());
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Clicked to send udp message");
            SendMessage();
        }
    }

    private void OnApplicationQuit()
    {
        client.Close();
    }

    public void SendMessage()
    {
        string msg = "Hello UDP Server from Unity";
        byte[] data = Encoding.ASCII.GetBytes(msg);
        client.Send(data, data.Length);
        client.SendAsync(data, data.Length);
        Debug.Log("Send to client");
        Debug.Log(client);
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        State d = new State(); 
        while (true)
        {

            try
            {
                // Bytes empfangen.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                string text = Encoding.UTF8.GetString(data);

                // Den abgerufenen Text anzeigen.
                print(">> " + text);

                //// latest UDPpacket
                //lastReceivedUDPPacket = text;

                //// ....
                //allReceivedUDPPackets = allReceivedUDPPackets + text;

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("hi");
    }
}
