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
    class Connection
    {
        Dictionary<string, GameObject> listSend  =new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> listGet = new Dictionary<string, GameObject>();
        IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
        JavaScriptSerializer jsonx = new JavaScriptSerializer();
        TcpClient tcpclnt = new TcpClient();
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
                tcpclnt.Connect(ip); //Connect to the server
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
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(jsonx.Serialize(data["HQ"]));
                //Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                stm.Flush();
                
            }
        }

        public Dictionary<string, GameObject> GetData()
        {
            Task a = new Task(GetDataA);
            a.Start();
            return listGet;
        }

        async void GetDataA()
        {
            Task<string> task = ProcessData(ConnStatus, tcpclnt);
            string data = await task;

            //Dictionary<string, object> routes_list = (Dictionary<string, object>)jsonx.DeserializeObject(data);
            //foreach (var obj in routes_list)
            //{
            //    var ob = obj.Value as GameObject;
            //    int x = 0;
            //}
            GameObject obj = (GameObject)jsonx.DeserializeObject(data);
            int x = 0;
        }

        static async Task<string> ProcessData(Status ConnStatus, TcpClient tcpClient)
        {
            if (ConnStatus == Status.Connected)
            {
                NetworkStream ns = tcpClient.GetStream();
                byte[] message = new byte[25000];
                int bytesRead;

                bytesRead = ns.Read(message, 0, 25000);
                ASCIIEncoding encoder = new ASCIIEncoding();

                string bufferincmessage = encoder.GetString(message, 0, bytesRead);
                
                Console.WriteLine(bufferincmessage); //Write the data on the screen

                return bufferincmessage;
            }
            return "";
        }
    }
}
