using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTS.Server
{
    class Program
    {

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9999);
            TcpClient client;
            listener.Start();

            Console.WriteLine("Waiting for a client...");
            while (true) // Add your exit flag here
            {
                client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ThreadProc, client);
            }
        }

        private static void ThreadProc(object obj)
        {

            var client = (TcpClient) obj;
            NetworkStream ns = client.GetStream();
            Console.WriteLine("Client connected...");
            byte[] message = new byte[25000];
            int bytesRead;

            while (client.Connected)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = ns.Read(message, 0, 25000);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                string bufferincmessage = encoder.GetString(message, 0, bytesRead);

                //Console.WriteLine(bufferincmessage);

                byte[] buffer = encoder.GetBytes(bufferincmessage);

                ns.Write(buffer, 0, buffer.Length);
                ns.Flush();
            }
        
    }

        //static void Main(string[] args)
            //{
            //    IPEndPoint ip = new IPEndPoint(IPAddress.Any, 9999); //Any IPAddress that connects to the server on any port
            //    IPEndPoint ip2 = new IPEndPoint(IPAddress.Any, 9998);
            //    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Initialize a new Socket
            //    Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Initialize a new Socket

            //    socket.Bind(ip); //Bind to the client's IP
            //    socket2.Bind(ip2);
            //    socket.Listen(10);
            //    socket2.Listen(10);//Listen for maximum 10 connections
            //    Socket client;
            //    Socket client2;
            //    Console.WriteLine("Waiting for a client...");
            //    client = socket.Accept();
            //    IPEndPoint clientep = (IPEndPoint) client.RemoteEndPoint;
            //    Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);

            //    Console.WriteLine("Waiting for a client 2...");
            //    client2 = socket2.Accept();
            //    IPEndPoint clientep2 = (IPEndPoint)client.RemoteEndPoint;
            //    Console.WriteLine("Connected with {0} at port {1}", clientep2.Address, clientep2.Port);

            //    string welcome = "Welcome"; //This is the data we we'll respond with
            //    byte[] data = new byte[1024];
            //    data = Encoding.ASCII.GetBytes(welcome); //Encode the data
            //    client.Send(data, data.Length, SocketFlags.None); //Send the data to the client
            //    while (client.Connected)
            //    {
            //        data = new byte[1024];
            //        int receivedDataLength = client.Receive(data); //Wait for the data
            //        string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength); //Decode the data received
            //        //Console.WriteLine(stringData); //Write the data on the screen
            //    }
            //    Console.WriteLine("Disconnected from {0}", clientep.Address);
            //    client.Close(); //Close Client
            //    socket.Close(); //Close socket
            //    Console.ReadKey();
            //}
        
    }
}
