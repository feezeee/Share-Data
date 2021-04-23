using System;
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

        static List<Local> LocalAddresses = CurrentIP.LocalIP;
        public static void SendBroadcastOfferToConnect() // функция которая отправлет широковещательное сообщение 
        {
            while(true)
            {
                foreach(var localaddress in LocalAddresses)
                //string BroadIP = "192.168.0.255";
                {
                    // создаем соект для работы по пратоколу UDP, в сети Internet, для передачи дейтаграмных сообщений
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPAddress broadcast = IPAddress.Parse(localaddress.BroadIP);

                    var message = SelfName.Name;
                    byte[] sendbuf = Encoding.ASCII.GetBytes(message);// кодируем сообщение из строки в битовый массив
                    IPEndPoint ep = new IPEndPoint(broadcast, remotePort);// создаем полыный адрес получателя, тоесть добавляем к IP еще и прот

                    s.SendTo(sendbuf, ep);// отправлем сообщение на адрес получателя
                    // Console.WriteLine("Message was sent to the broadcast address");

                    Thread.Sleep(5000);
                }
            }
        }
        
        private static bool IsLocalAddress(string addres)
        {

            foreach (var localaddress in LocalAddresses)
            {
                if (addres == localaddress.LocalIP)
                {
                    return true;
                }
            }

            return false;
        }
        public static void ReciveBroadcastOffer(object available)
        {
            UdpClient listener = new UdpClient(localPort); // для прослушивания сообщений udp приходящих на локальный порт
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, localPort); // адрес приема, для приема всех сообщений
            try
            {   
                while (true)
                {
                    // Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP); // получаем сообщение
                    
                    if (IsLocalAddress(groupEP.Address.ToString())) continue;
                    var name = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    AvailableConection availableConection = (AvailableConection)available;                    
                    availableConection.AddMember(groupEP.Address, name);
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

        public static void GetConnect(object available) // класс интерфейс
        {
            //Thread receiveThread = new Thread(new ThreadStart(ReciveBroadcastOffer)); //созадем новый поток отдельно для получения
            //receiveThread.Start(); // начинаем слушать сеть
            Thread sendThread = new Thread(new ThreadStart(SendBroadcastOfferToConnect)); //созадем новый поток отдельно для получения
            sendThread.Start(); // запускаем процесс отправки сообщений

            Thread receiveThread = new Thread(new ParameterizedThreadStart(ReciveBroadcastOffer));
            receiveThread.Start(available);
        }//Не используем
    }
}
