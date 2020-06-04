using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Interface
{
    class ConnectionV2
    {
        const int localPort = 8010; // порт для приема информации
        const int remotePort = 8010; // порт для отправки информации
        // данный адрес необходимо будет получать, ибо он может варироваться, но пока с кастылем
        static string broadcastingIP = "192.168.1.255"; // широковещательный адрес локальной сети
        // данный адрес необходимо будет получать, ибо он может варироваться, но пока с кастылем
        static IPAddress localIP = IPAddress.Parse("192.168.1.9");// лакальный адресс

        static void SendBroadcastOfferToConnect() // функция которая отправлет широковещательное сообщение 
        {
            while(true)
            {
                // создаем соект для работы по пратоколу UDP, в сети Internet, для передачи дейтаграмных сообщений
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);  

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

                    Console.WriteLine($"Received broadcast from {groupEP.Address} :"); // вывод адреса атправителя
                    Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}"); //вывод соообщения атправителя, предвор дешифр
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
            SendBroadcastOfferToConnect(); // запускаем процесс отправки сообщений
        }
    }
}
