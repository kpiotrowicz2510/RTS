using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RTS.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 9999); //Any IPAddress that connects to the server on any port
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Initialize a new Socket

            socket.Bind(ip); //Bind to the client's IP
            socket.Listen(10); //Listen for maximum 10 connections
            Socket client;
            
                Console.WriteLine("Waiting for a client...");
                client = socket.Accept();
                IPEndPoint clientep = (IPEndPoint) client.RemoteEndPoint;
                Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);
            string welcome = "Welcome"; //This is the data we we'll respond with
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(welcome); //Encode the data
            client.Send(data, data.Length, SocketFlags.None); //Send the data to the client
            while (client.Connected)
            {
                data = new byte[1024];
                int receivedDataLength = client.Receive(data); //Wait for the data
                string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength); //Decode the data received
                Console.WriteLine(stringData); //Write the data on the screen
            }
            Console.WriteLine("Disconnected from {0}", clientep.Address);
            client.Close(); //Close Client
            socket.Close(); //Close socket
            Console.ReadKey();
        }
    }
}
