using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public class TcpSender
    {
        private int port = 3200;
        public IPAddress RemoteIP;

        public TcpSender(string ip)
        {
            RemoteIP = IPAddress.Parse(ip);
        }

        public void SendFileRequest(string localPathToSave, string remoteFilePath)
        {
            var reciverEP = new IPEndPoint(RemoteIP, port);
            var client = new TcpClient();
            client.Connect(reciverEP);
            while(!client.Connected);
            var connectedStream = client.GetStream();

            //Request.DoSendingRequest()
            var mess = $"Request {(int)RequestTipe.SendFile} {remoteFilePath}";
            var buf = Encoding.UTF8.GetBytes(mess);
            connectedStream.Write(buf, 0, buf.Length);

            Int64 bytesReceived = 0;
            int count;
            byte[] buffer = new byte[1024 * 8];
            connectedStream.Read(buffer, 0, 1024);
            Int64 numberOfBytes = BitConverter.ToInt64(buffer, 0);

            using (var fileIO = File.Create(localPathToSave))
            {
                while (bytesReceived < numberOfBytes && (count = connectedStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileIO.Write(buffer, 0, count);

                    bytesReceived += count;
                }
            }

            client.Close();
            connectedStream.Close();
        }
    }
}
