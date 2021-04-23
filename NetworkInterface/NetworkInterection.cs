using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyNetworkInterface
{
    public class NetworkInterection
    {
        const int localPort = 8010; // порт для приема информации
        const int remotePort = 8010; // порт для отправки информации



        public IP CurentPC;
        
        
        /// <summary>
        /// The sending message to a broadcast address
        /// </summary>
        public void SendBroadcastOfferToConnect() // функция которая отправлет широковещательное сообщение 
        {
            try
            {
                while (true)
                {

                    //foreach (var localaddress in CurentPC)
                    //string BroadIP = "192.168.0.255";
                    //{
                    // создаем соект для работы по пратоколу UDP, в сети Internet, для передачи дейтаграмных сообщений

                    if (CurentPC != null && CurentPC.ReturnBroadcastAddress() != null && CurentPC.ReturnIpAddress() != null && CurentPC.ReturnSubnetMask() != null && CurentPC.ReturnNameInNetwork() != null)
                    {
                        Socket socketForBroadcasting = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        IPAddress broadcast = CurentPC?.ReturnBroadcastAddress();

                        var message = CurentPC.ReturnNameInNetwork();


                        byte[] sendbuf = Encoding.ASCII.GetBytes(message);// кодируем сообщение из строки в битовый массив
                        IPEndPoint ep = new IPEndPoint(broadcast, remotePort);// создаем полыный адрес получателя, тоесть добавляем к IP еще и прот

                        socketForBroadcasting.SendTo(sendbuf, ep);// отправлем сообщение на адрес получателя
                                                                  // Console.WriteLine("Message was sent to the broadcast address");
                        Thread.Sleep(5000);
                    }
                    //}
                }
            }
            catch { }
        }

        public void ReciveBroadcastOffer(object available)
        {
            UdpClient listener = new UdpClient(localPort); // для прослушивания сообщений udp приходящих на локальный порт
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, localPort); // адрес приема, для приема всех сообщений
            try
            {
                while (true)
                {
                    // Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP); // получаем сообщение

                    IPParameters recivedPC = new IPParameters();
                    var name = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    recivedPC.SetNameInNetwork(name);
                    recivedPC.SetIpAddress(groupEP.Address);
                    recivedPC.SetBroadcastAddress(CurentPC.ReturnBroadcastAddress());
                    recivedPC.SetSubnetMask(CurentPC.ReturnSubnetMask());

                    if (!CurentPC.FindningInformationAboutPCInList(recivedPC))//Обрабатываем сообщения, принимаемые от этого же пк
                    {

                    }

                    
                }


                AvailableConection availableConection = (AvailableConection)available;
                availableConection.AddMember(groupEP.Address, name);
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
    }
}
