using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public static class TcpServer
    {
        private static int port = 3200;
        private static IPAddress listningIP = IPAddress.Any;

        public static void ListenRequest()
        {
            TcpListener server = new TcpListener(listningIP, port);
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

                    Request.ExecuteRecuest(request, connectedStream);


                    getter.Close();
                    connectedStream.Close();
                }

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            finally
            {
                server.Stop();
            }
        }
    }
}
