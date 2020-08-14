using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace InterfaceV2
{
    public class TcpFileClient
    {
        private static int port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);

        public delegate void SendingMethod(double procents);
        public event SendingMethod SendingEvent;

        public IPAddress RemoteIP;
        private TcpClient client = new TcpClient();
        public TcpFileClient(string ip)
        {
            RemoteIP = IPAddress.Parse(ip);
        }

        public void SendRequest(RequestTipe request, string messege)
        {
        }

        /// <summary>
        /// куда|откуда
        /// </summary>
        /// <param name="obj"></param>
        public void SendFileRequest (object obj)
        {
            string[] mes = obj.ToString().Split('|');
            string localPathToSave = mes[0];
            string remoteFilePath = mes[1];
            var reciverEP = new IPEndPoint(RemoteIP, port);
            client.Connect(reciverEP);
            while (!client.Connected);
            var connectedStream = client.GetStream();

            //Request.DoSendingRequest()
            var mess = $"Request|{(int)RequestTipe.SendFile}|{remoteFilePath}";
            var buf = Encoding.UTF8.GetBytes(mess);
            connectedStream.Write(buf, 0, buf.Length);

            Int64 bytesReceived = 0;
            int count;
            byte[] buffer = new byte[1024*8];  
            connectedStream.Read(buffer, 0, 1024);           
            

            Int64 fileBytesSize = BitConverter.ToInt64(buffer, 0);

            if(fileBytesSize == -1)
            {

            } // файл был не найден
            if(fileBytesSize == -2)
            {

            }// запрос небыл обработан
            bool peremen = Execute_(localPathToSave);
            if(peremen)
            using (var fileIO = File.Create(localPathToSave))
            {
                while (bytesReceived < fileBytesSize && (count = connectedStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileIO.Write(buffer, 0, count);

                    bytesReceived += count;
                        double first = bytesReceived; double second = fileBytesSize;
                    SendingEvent?.Invoke(first/second*100);
                }
                SendingEvent?.Invoke(100);
            }

            connectedStream.Close();
            client.Close();
        }
        private bool Execute_(string path)
        {
            try
            {
                string[] mes = path.Split('\\');
                string ans = mes[0]+'\\';
                //int request = int.Parse(mes[1]);
                for (int i = 1; i < mes.Length - 1; i++)
                {
                    ans += mes[i]+'\\';
                    if (Directory.Exists(ans)==false)
                    {
                        Directory.CreateDirectory(ans);
                    }
                }
                return true;
            }
            catch            
            {
                return false;
            }
        }
    }
}
