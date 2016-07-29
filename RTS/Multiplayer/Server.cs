using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using RTS.Abstract;

namespace RTS.Multiplayer
{
    class Server
    {
        IPEndPoint ip = new IPEndPoint(IPAddress.Any, 9999); //Any IPAddress that connects to the server on any port
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Initialize a new Socket
        JavaScriptSerializer jsonx = new JavaScriptSerializer();
        public Status ConnStatus;
        public Socket client;
        private string dataStream;
        public Server( int port)
        {
            ip = new IPEndPoint(IPAddress.Any, port);
            socket.Bind(ip); //Bind to the client's IP
            socket.Listen(10); //Listen for maximum 10 connections
        }

        public void Start()
        {
            Thread newThread = new Thread(init);
            ConnStatus = Status.Disconnected;
            newThread.Start();
        }

        public void init()
        {
            Console.WriteLine("Waiting for a client...");
            client = socket.Accept();
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);
            ConnStatus=Status.Connected;
            while (true)
            {
                GetData();
            }

            string welcome = "Welcome"; //This is the data we we'll respond with
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(welcome); //Encode the data
            client.Send(data, data.Length, SocketFlags.None); //Send the data to the client
            Console.WriteLine("Disconnected from {0}", clientep.Address);

        }
        public void End()
        {
            client.Close(); //Close Client
            socket.Close(); //Close socket
        }
        public void SendData(IEnumerable<KeyValuePair<string, GameObject>> data)
        {
            //listSend = data;
            if (ConnStatus == Status.Connected)
            {
                client.Send(Encoding.ASCII.GetBytes(jsonx.Serialize(data.ToList()) + "#"));
                //client.Blocking = true;
            }
        }
        public string GetDataX()
        {
            if (ConnStatus == Status.Connected)
            {
                Task k = new Task(GetData);
                k.Start();
                //client.Blocking = false;
            }
            
            return dataStream?.Split('#')[0];
        }
        public async void GetData()
        {
            Task<string> task = DataGet(client);
            string data = await task;
            dataStream = data;
        }

        static async Task<string> DataGet(Socket client)
        {
            byte[] data = new byte[10096];
            int receivedDataLength = client.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength); //Decode the data received
            //Console.WriteLine(stringData); //Write the data on the screen
            return stringData;
        }
    }
}
