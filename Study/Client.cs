using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        const int PortNum = 25000;

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            Console.WriteLine("Connecting to server");
            client.Connect(IPAddress.Loopback, PortNum);
            Console.WriteLine("Connected");

            var socket = client.Client;
            string line;
            do
            {
                line = Console.ReadLine();
                if (line != "")
                {
                    try
                    {
                        socket.Send(Encoding.UTF8.GetBytes(line));
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("Send failed");
                        if (!socket.Connected)
                            break;
                    }
                }
            } while (line != "");

            socket.Disconnect(false);

            client.Close();
            Console.WriteLine("Closed connection. Press any key");
            Console.ReadKey();
        }
    }
}
