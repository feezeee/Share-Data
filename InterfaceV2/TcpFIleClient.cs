using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace InterfaceV2
{
    public class TcpFIleClient
    {
        private static int port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);
        public IPAddress RemoteIP;
        private TcpClient client = new TcpClient();
        public TcpFIleClient(string ip)
        {
            RemoteIP = IPAddress.Parse(ip);
        }

        public void SendRequest(RequestTipe request, string messege)
        {

        }
        public void SendFileRequest(string localPathToSave, string remoteFilePath)
        {
            var reciverEP = new IPEndPoint(RemoteIP, port);
            client.Connect(reciverEP);
            while (!client.Connected);
            var connectedStream = client.GetStream();

            //Request.DoSendingRequest()
            var mess = $"Request {(int)RequestTipe.SendFile} {remoteFilePath}";
            var buf = Encoding.UTF8.GetBytes(mess);
            connectedStream.Write(buf, 0, buf.Length);

            Int64 bytesReceived = 0;
            int count;
            byte[] buffer = new byte[1024 * 8];
            connectedStream.Read(buffer, 0, 1024);
            Int64 fileBytesSize = BitConverter.ToInt64(buffer, 0);

            if(fileBytesSize == -1)
            {

            } // файл был не найден
            if(fileBytesSize == -2)
            {

            }// запрос небыл обработан
            using (var fileIO = File.Create(localPathToSave))
            {
                while (bytesReceived < fileBytesSize && (count = connectedStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileIO.Write(buffer, 0, count);

                    bytesReceived += count;
                }
            }

            connectedStream.Close();
            client.Close();
        }
    }
}
