﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IPmanip;

namespace Interface
{
    public class ConnectionV2
    {
        const int localPort = 8010; // порт для приема информации
        const int remotePort = 8010; // порт для отправки информации
        static string broadcastingIP = CurrentIP.BroadIP; // широковещательный адрес локальной сети
        static IPAddress localIP = IPAddress.Parse(CurrentIP.LocalIP);// лакальный адресс

        static void SendBroadcastOfferToConnect() // функция которая отправлет широковещательное сообщение 
        {
            while(true)
            {
                // создаем соект для работы по пратоколу UDP, в сети Internet, для передачи дейтаграмных сообщений
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //localIP = localIP;
                IPAddress broadcast = IPAddress.Parse(broadcastingIP);

                byte[] sendbuf = Encoding.ASCII.GetBytes("offer to conect");// кодируем сообщение из строки в битовый массив
                IPEndPoint ep = new IPEndPoint(broadcast, remotePort);// создаем полыный адрес получателя, тоесть добавляем к IP еще и прот

                s.SendTo(sendbuf, ep);// отправлем сообщение на адрес получателя
                Console.WriteLine("Message sent to the broadcast address");

                Thread.Sleep(5000);
                //for (int i = 0; i < 1000000000; i++) ; // задержка между рассылкой сообщение калхоз вариант
            }
        }
        static void ReciveBroadcastOffer()
        {
            UdpClient listener = new UdpClient(localPort); // для прослушивания сообщений udp приходящих на локальный порт
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, localPort); // адрес приема, для приема всех сообщений
            try
            {   
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP); // получаем сообщение
                    if (localIP.ToString() == groupEP.Address.ToString()) continue;
                    var name = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    AvailableConection.AddMember(groupEP.Address, name);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }

        public static void GetConnect() // класс интерфейс
        {
            Thread receiveThread = new Thread(new ThreadStart(ReciveBroadcastOffer)); //созадем новый поток отдельно для получения
            receiveThread.Start(); // начинаем слушать сеть
            Thread sendThread = new Thread(new ThreadStart(SendBroadcastOfferToConnect)); //созадем новый поток отдельно для получения
            sendThread.Start(); // запускаем процесс отправки сообщений
        }
    }
}
