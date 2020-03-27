using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShips
{
    class Connection
    {
        public delegate void FieldChoosen(Field field);
        public event FieldChoosen gotField;
        public char spliter = ',';
        NetworkStream ns;
        TcpClient client;
        //TcpClient client;
        public void Connect()
        {
            IPAddress ip = IPAddress.Parse(GetLocalIPAddress());
            int port = 5000;
            client = new TcpClient();
            client.Connect(ip, port);
            ns = client.GetStream();

            Thread thread = new Thread(ReceiveData);
            thread.Start();          

            //client.Client.Shutdown(SocketShutdown.Send);
            //thread.Join();
            //ns.Close();
            //client.Close();
            //Console.WriteLine("disconnect from server!!");
            //Console.ReadKey();
        }

        public void SendMessage(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            ns.Write(buffer, 0, buffer.Length);
        }

        string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        void ReceiveData()
        {
            NetworkStream net = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            string temp;
            while ((byte_count = net.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                temp = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
                Console.WriteLine("Message");
                //Console.WriteLine("temp " + temp + "text " + text);
                temp = temp.Replace("\0", String.Empty);
                Field field = new Field();
                field.X = int.Parse(temp.Split(',')[0]);
                field.Y = int.Parse(temp.Split(',')[1]);
                field.FieldCharacter = 'Y';
                
                if(gotField != null)
                    gotField(field);
            }
        }
    }
}
