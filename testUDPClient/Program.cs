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
        Socket socket;
        private static byte[] data = new byte[1024];
        void Start()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18500);
            EndPoint ep = (EndPoint)ipep;
            socket = new Socket(AddressFamily.InterNetwork,
                      SocketType.Dgram, ProtocolType.Udp);
            socket.Bind((EndPoint)ipep);


            while (true)
            {
                byte[] data = new byte[1024];
                int recv = socket.ReceiveFrom(data, ref ep);

                string b = Encoding.ASCII.GetString(data, 0, 1); // Received Data
                Console.WriteLine("Received " + b);
            }
        }

        // Asynchronous Receiver is not working and is somewhat buggy. Do we even need this at this point?
        void AsyncStart()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18500);
            EndPoint ep = (EndPoint)ipep;
            socket = new Socket(AddressFamily.InterNetwork,
                      SocketType.Dgram, ProtocolType.Udp);
            socket.Bind((EndPoint)ipep);

            data = new byte[1024];
            Console.WriteLine("Async Start Begin Receive ! ");
            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

        }
        
        void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR); // the exception is thrown here
            byte[] dataBuf = new byte[received];
            Array.Copy(data, dataBuf, received);

            Console.WriteLine("Async received! " + Encoding.ASCII.GetString(dataBuf));
            //Console.WriteLine(Encoding.ASCII.GetString(dataBuf));

            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program my = new Program();
            my.Start();
            //my.AsyncStart();
        }
    }
}
