using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Interface
{
    class Interface
    {
        static int port = 2010;
        public static void Test()
        {
            Thread sendThread = new Thread(new ThreadStart(SendRequest));
            Thread recieveThread = new Thread(new ThreadStart(Recieve));

            
            sendThread.Start();
            recieveThread.Start();
        }

        public static void SendRequest()
        {
            IPAddress recieverIP = IPAddress.Parse("127.0.0.1");
            try
            {
                Socket requester = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint remoteRecieverEP = new IPEndPoint(recieverIP, port);
                requester.Connect(remoteRecieverEP); // соединиение с устройством, для отправки запроса
                string requestMessage = "request ";
                byte[] byteRequestMess = Encoding.Unicode.GetBytes(requestMessage);
                requester.Send(byteRequestMess); //отправка сообщения

                //ожидание ответа о доставке
                byte[] data = new byte[256]; // буфер для ответа
                StringBuilder ansBuilder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                //var reciever = requester.Accept(); // дополнительный сокет для получения ответа
                do
                {
                    bytes = requester.Receive(data);
                    ansBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (requester.Available > 0);
                
                if(ansBuilder.ToString() == "accept")
                {
                    Console.WriteLine("request was veryfied");
                }
                
                requester.Shutdown(SocketShutdown.Both);
                //reciever.Shutdown(SocketShutdown.Both);
                //reciever.Close();
                requester.Close();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public static void Send()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                Console.Write("Введите сообщение:");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("ответ сервера: " + builder.ToString());

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Recieve()
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                    // отправляем ответ
                    string message = "accept";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SendFile(IPEndPoint remoteConnection, string fileName)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(remoteConnection);
                Console.WriteLine("Sending {fileName} to the {remoteIP}");
                client.SendFile(fileName);
            }
            finally
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            
        }
        static void Listen()
        {

        }
    }
}
