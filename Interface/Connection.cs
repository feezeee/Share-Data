using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Interface
{
    class Connection
    {
        const int localPort = 8010;
        const int remotePort = 8010;
        //static IPAddress BroadcustingAddress = IPAddress.Parse("224.0.0.251");
        static IPAddress BroadcustingAddress = IPAddress.Parse("224.0.0.2");
        //$netsh interface ip delete arpcache команда для очистки таблицы arp
        //static IPAddress BroadcustingAddress = IPAddress.Parse("226.0.0.22");

        const string m= "";
        public static void SendBroadcastOfferToConnect()
        {
            UdpClient client = new UdpClient();
            client.EnableBroadcast = true;
            IPEndPoint endPoint = new IPEndPoint(BroadcustingAddress, remotePort);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, remotePort);
           
            try
            {
                while (true)
                {
                    string message = "offer to connect form sergey";
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    client.Send(data, data.Length, endPoint);
                }
            }
            finally
            {
                client.Close();
            }
        }
        public static void ReciveBroadcastOffer()
        {
            UdpClient receiver = new UdpClient(localPort); // UdpClient для получения данных
            //receiver.MulticastLoopback = false;
            receiver.JoinMulticastGroup(BroadcustingAddress);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, remotePort);


            IPEndPoint remoteIp = null;
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);
                    Console.WriteLine(message+ " " + remoteIp.Address.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }

        public static void GetConnect()
        {
            Thread receiveThread = new Thread(new ThreadStart(ReciveBroadcastOffer));
            receiveThread.Start();
            SendBroadcastOfferToConnect(); // отправляем сообщение
        }
    }
}
