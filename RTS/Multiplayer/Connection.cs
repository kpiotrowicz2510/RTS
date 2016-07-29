using System;
using System.Collections.Generic;
using System.IO;
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
    public enum Status
    {
        Connected, Disconnected, ConnError
    }
    class Connection
    {
        IEnumerable<KeyValuePair<string, GameObject>> listSend;
        IEnumerable<KeyValuePair<string, GameObject>> listGet =null;
        public IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
        JavaScriptSerializer jsonx = new JavaScriptSerializer();
        TcpClient tcpclnt = new TcpClient();
        public Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Status ConnStatus;
        
  
        public Connection()
        {
 
        }

        public void Close()
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            ConnStatus = Status.Disconnected;
        }
        public void Connect()
        {
            try
            {
                server.Connect(ip); //Connect to the server
                ConnStatus = Status.Connected;
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                ConnStatus = Status.ConnError;
                return;
            }
        }

        public void SendData(IEnumerable<KeyValuePair<string, GameObject>> data)
        {
            listSend = data;
            if (ConnStatus == Status.Connected)
            {
                server.Send(Encoding.ASCII.GetBytes(jsonx.Serialize(data.First().Value)));
            }
        }

        public IEnumerable<KeyValuePair<string, GameObject>> GetData()
        {
            //Task a = new Task(GetDataX);
            //a.Start();
            return listGet;
        }

        async void GetDataX()
        {
            Task<string> task = DataGet(server);
            string data = await task;

        }

        static async Task<string> DataGet(Socket client)
        {
            byte[] data = new byte[4096];
            int receivedDataLength = client.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength); //Decode the data received
            //Console.WriteLine(stringData); //Write the data on the screen
            return stringData;
        }
    }
}
