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
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            }
        }

        void AsyncStart()
        {

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program my = new Program();
            my.Start();
        }
    }
}
