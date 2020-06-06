using IPmanip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public class RequestInteractivity
    {
        private static IPAddress localIP = IPAddress.Parse(CurrentIP.LocalIP);
        private static int port = 2210; 
        private static string GetRequestString(RequestTipe request, string requestMess)
        {
            string requestString = $"Request {localIP} " + ((int)request).ToString() + requestMess;
            return requestString;
        }
        private static string GetAnswerString(string requestMess)
        {
            string requestString = $"Answer {localIP} "+ requestMess;
            return requestString;
        }
        public static void SendRequst(string reciverIPstr, RequestTipe request, string requestMess)
        {
            try
            {
                IPAddress recieverIP = IPAddress.Parse(reciverIPstr);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint recieverEP = new IPEndPoint(recieverIP, port);
                sender.Connect(recieverEP);

                byte[] message = Encoding.Unicode.GetBytes(GetRequestString(request, requestMess));
                sender.Send(message);

                Request.DoAfterSend(request);

                byte[] data = new byte[256]; // буфер для ответа
                StringBuilder ansBuilder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = sender.Receive(data);
                    ansBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (sender.Available > 0);

                if (ansBuilder.ToString() == "accept")
                {
                    Console.WriteLine("request was veryfied");
                }

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DoRequestsRecieveing()
        {
            try 
            { 
                Socket reciever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
                reciever.Bind(localEP); // связываем сокет с точкой принимающей все данные приход на прот port
                reciever.Listen(100); // очередь на прием 100

                while (true)
                {
                    var getter = reciever.Accept(); // получение следующего сообщения в очереди
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = getter.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (getter.Available > 0);

                    string requestMess = builder.ToString();

                    if (requestMess.IndexOf("Request") != -1)
                    {
                        string[] mess = requestMess.Split(' ');
                        var IP = IPAddress.Parse(mess[1]);
                        var ans = Request.ExecuteRecuest(requestMess);
                        byte[] message = Encoding.Unicode.GetBytes(GetAnswerString(ans));
                        getter.Send(message);
                    }


                    getter.Shutdown(SocketShutdown.Both);
                    getter.Close();
                }
                
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
