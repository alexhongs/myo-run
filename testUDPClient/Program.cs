using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System;

namespace UDPClient
{
    class Program
    {
        UdpClient client;
        IPAddress serverIP;
        string hostIP = "127.0.0.1";
        int port = 18500;
        Socket socket;
        Byte[] buffer;
        EndPoint epServer;
        void Start()
        {
            Console.WriteLine("Starting Client");
            serverIP = IPAddress.Parse(hostIP);
            IPEndPoint server = new IPEndPoint(serverIP, port);
            epServer = (EndPoint)server;


            client = new UdpClient();
            client.Connect(server);
            
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            client.Send(sendBytes, sendBytes.Length);

            // Inefficient algorithm but working version
            while (true)
            {
                Byte[] receiveBytes = client.Receive(ref server);
                string message = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine(message);
                //Console.WriteLine("End Receive Data");
            }

            //client.BeginReceive(new AsyncCallback(this.ReceiveData), null);
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //buffer = new byte[1024];
            //Console.WriteLine("Setup Receive Data");
            //socket.BeginReceive(buffer, 0, 1024, SocketFlags.None, new AsyncCallback(this.ReceiveData), null);
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                // Receive all data
                int x = this.socket.EndReceive(ar);

                //// Initialise a packet object to store the received data
                //Packet receivedData = new Packet(this.dataStream);

                //// Update display through a delegate
                //if (receivedData.ChatMessage != null)
                //    this.Invoke(this.displayMessageDelegate, new object[] { receivedData.ChatMessage });
                string message = Encoding.ASCII.GetString(buffer, 0, x);
                Console.WriteLine("Receiving Data");
                Console.WriteLine(message);

                // Reset data stream
                this.buffer = new byte[1024];

                // Continue listening for broadcasts
                socket.BeginReceiveFrom(this.buffer, 0, this.buffer.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                //MessageBox.Show("Receive Data: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program my = new Program();
            my.Start();
        }
    }
}
