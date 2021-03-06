﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static void Main(string[] args)
        {
            Console.WriteLine("Server Started");
            int count = 1;
            IPAddress ip = IPAddress.Parse(GetLocalIPAddress());
            TcpListener ServerSocket = new TcpListener(ip, 5000);
            ServerSocket.Start();

            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock)
                    list_clients.Add(count, client);
                Console.WriteLine("Someone connected!!");

                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;
            }
        }

        public static string GetLocalIPAddress()
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

        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            while (true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count;

                try
                {
                    byte_count = stream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }


                if (byte_count == 0)
                {
                    break;
                }

                // This will handle player REQUESTS
                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                // Sets nesseary data to the rest of the players
                broadcast(buffer);
                
                // Print for debug info
                Console.WriteLine(data);
            }

            lock (_lock)
                list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public static void broadcast(byte[] data)
        {
            //byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);
            byte[] buffer = data;

            lock (_lock)
            {
                foreach (TcpClient c in list_clients.Values)
                {
                    NetworkStream stream = c.GetStream();

                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
