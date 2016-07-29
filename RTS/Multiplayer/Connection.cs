using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using RTS.Abstract;


namespace RTS.Multiplayer
{
    class Connection
    {
        Dictionary<string, GameObject> listSend  =new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> listGet = new Dictionary<string, GameObject>();
        IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
        JavaScriptSerializer jsonx = new JavaScriptSerializer();
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Status ConnStatus;
        public enum Status
        {
            Connected,Disconnected,ConnError
        }
  
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

        public void SendData(Dictionary<string, GameObject> data)
        {
            listSend = data;
            if (ConnStatus == Status.Connected)
            {
                server.Send(Encoding.ASCII.GetBytes(jsonx.Serialize(data)));
            }
        }

        public Dictionary<string, GameObject> GetData()
        {
            if (ConnStatus == Status.Connected)
            {
                byte[] data = new byte[1024];
                int receivedDataLength = server.Receive(data); //Wait for the data
                string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength); //Decode the data received
                Console.WriteLine(stringData); //Write the data on the screen
            }
            return listGet;
        }
    }
}
