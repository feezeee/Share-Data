using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace InterfaceV2
{
    public class TcpServer
    {
        private static int port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);
        private static IPAddress listningIP = IPAddress.Any;
        private static TcpListener server = new TcpListener(listningIP, port);




        public Request myRequest = new Request();
        public void StopServer()
        {
            if(server != null && server.Pending())
            {
                server.Stop();
            }
        }
        
           /// <summary>
           /// Ожидание подключения и последующее выполнение указанной операции
           /// </summary>
        public async void ListenRequest()
        {
            await Task.Run(()=>
            {

                try
                {
                    server.Start();
                    while (true)
                    {
                        var getter = server.AcceptTcpClient();
                        var connectedStream = getter.GetStream();

                        var buf = new byte[256];
                        var requestLen = connectedStream.Read(buf, 0, buf.Length);
                        string request = Encoding.UTF8.GetString(buf, 0, requestLen);

                        myRequest.ExecuteRecuest(request, "0", connectedStream);
                        getter.Close();
                        connectedStream.Close();
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    StopServer();
                }
            });
        }
    }
}
