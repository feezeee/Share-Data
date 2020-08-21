using IPmanip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public static class RequestInteractivity
    {
        private static IPAddress localIP = IPAddress.Parse("127.0.0.1");
        private static int port = 2210; 
        private static string GetRequestString(RequestTipe request, string requestMess)
        {
            string requestString = $"Request|" + ((int)request).ToString() + '|' + requestMess;
            return requestString;
        }
        private static string GetAnswerString(string requestMess)
        {
            string requestString = $"Answer|"+ requestMess;
            return requestString;
        }
        public static string SendRequst(string reciverIPstr, RequestTipe request = RequestTipe.GetDirectoryFiles, string requestMess = "ans")
        {
            string Response = "";
            try
            {
                IPAddress recieverIP = IPAddress.Parse(reciverIPstr);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint recieverEP = new IPEndPoint(recieverIP, port); // точка получателя
                sender.Connect(recieverEP);

                var requestM = GetRequestString(request, requestMess); // формирование сообщения запроса
                byte[] message = Encoding.Unicode.GetBytes(requestM);// отпрака запроса
                sender.Send(message);// отпрака сообщения на соединенный сокет
                Console.WriteLine($"запрос был отправлен на - {recieverEP.Address}");
                Console.WriteLine($"содержание запроса - {requestM}");

                //Request.DoAfterSend(request); // действие которое необходимо сделать после отпраки запроса

                // получение ответа от получателя 
                byte[] data = new byte[256]; // буфер для ответа
                StringBuilder ansBuilder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {                    
                    bytes = sender.Receive(data);
                    ansBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    Thread.Sleep(100);
                }                
                while (sender.Available > 0);

                var response = ansBuilder.ToString();
                if (response.IndexOf("Answer") != -1)
                {
                    Console.WriteLine("request was veryfied");
                    Console.WriteLine("ответ получателя - " + response);
                }

                if(response.IndexOf($"{RequestError.DirectoryNotExist}") != -1)
                {
                    return RequestError.DirectoryNotExist.ToString();
                    //throw new Exception("дериктория не найдена");
                }

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                Response = response;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Response;
        }

        public static void DoRequestsRecieveing()//получает запросы приходящие на сервер
        {
            try 
            { 
                Socket reciever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
                reciever.Bind(localEP); // связываем сокет с точкой принимающей все данные приход на прот port
                reciever.Listen(100); // очередь на прием 100

                while (true)
                {
                    Console.WriteLine("оджидание запроса...");
                    
                    //if (reciever.Available == 0)
                    //{
                    //    Thread.Sleep(5000);
                    //    continue;
                    //}
                    var getter = reciever.Accept(); // получение следующего сообщения в очереди
                    Console.WriteLine("обработка запроса...");
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    // получение запроса от отправителя
                    do
                    {
                        bytes = getter.Receive(data); 
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (getter.Available > 0);

                    string requestMess = builder.ToString();

                    if (requestMess.IndexOf("Request") != -1)
                    {
                        Console.WriteLine("запрос отправителя - " + requestMess);
                        string[] mess = requestMess.Split('|');
                        IPAddress reciverIP = ((IPEndPoint)getter.RemoteEndPoint).Address; // получаем ip отправителя
                        Console.WriteLine("ip отправителя - " + reciverIP.ToString());

                        //происходит обработка запроса
                        var ans = Request.ExecuteRecuest(requestMess,reciverIP.ToString()); // получаем ответ на запрос
                        Console.WriteLine($"ответ на запрос {ans}");

                        byte[] message = Encoding.Unicode.GetBytes(GetAnswerString(ans));
                        getter.Send(message); // отправляем ответ
                    }


                    getter.Shutdown(SocketShutdown.Both);
                    getter.Close();
                    Thread.Sleep(5000);
                }
                
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void SendFile()
        {

        }
    }
}
