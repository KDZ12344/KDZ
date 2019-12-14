using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        const int PortNum = 25000;
        const int InBufferSize = 1024;

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 25000);
            listener.Start();

            Console.WriteLine("Waiting for connection");
            var socket = listener.AcceptSocket();
            Console.WriteLine("Client connected");

            byte[] buf = new byte[InBufferSize];

            do
            {
                if (socket.Available > 0)
                {
                    int received = socket.Receive(buf);
                    if (received > 0)
                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(buf, 0, received));
                }
            } while (!Console.KeyAvailable || Console.ReadKey(false).Key != ConsoleKey.Escape);

            socket.Disconnect(false);
            socket.Close();
            Console.WriteLine("Closed connection. Press any key");
            Console.ReadKey();
        }
    }
}
