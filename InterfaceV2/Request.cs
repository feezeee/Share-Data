using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace InterfaceV2
{

    public enum RequestTipe
    {
        SendFile = 1,
        GetDirectoryFiles,
        GetFileFromMe,
        CreateDirrectory,
        SendFileFromMe
    }
    public enum RequestError
    {
        FileNotExist = -1,
        DirectoryNotExist = -2,
        RequestIncomprehantable = -3,
        ReadyForReading = -4
    }
    public class Request
    {
        public delegate void SendingMethod(double procents);
        public event SendingMethod SendingEvent;

        public string DoSendingRequest(string requestMessage, NetworkStream stream = null)
        {
            string ans = "";

            string[] mes = requestMessage.Split('|');
            int request = int.Parse(mes[1]);
            switch (request)
            {
                case (int)RequestTipe.GetDirectoryFiles:
                    ans = GetDirectory(mes[2]);
                    break;
                case (int)RequestTipe.SendFile:
                    SendFile(mes[2], stream);
                    break;
                case (int)RequestTipe.GetFileFromMe:
                    //
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("request isn't distingushed");
                    break;
            }

            return ans;
        }
        public string ExecuteRecuest(string requestMessage, string IP = "0", NetworkStream stream = null)
        {
            string ans = "";

            string[] mes = requestMessage.Split('|');
            int request = int.Parse(mes[1]);
            switch (request)
            {
                case (int)RequestTipe.GetDirectoryFiles:
                    ans = GetDirectory(mes[2]);
                    break;
                case (int)RequestTipe.SendFile:
                    SendFile(mes[2], stream);
                    break;
                case (int)RequestTipe.GetFileFromMe:
                    ans = DoResend(mes[2], mes[3], IP);
                    break;
                case (int)RequestTipe.CreateDirrectory:
                    ans = DoCreateDirrectory(mes[2]);
                    break;
                case (int)RequestTipe.SendFileFromMe:
                    SendFileFromMe(mes[2],stream);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("request isn't distingushed");
                    return RequestError.RequestIncomprehantable.ToString();
            }

            return ans;
        }

        public string DoCreateDirrectory(string path)
        {
                           
            DirectoryInfo drInfo = new DirectoryInfo(path);
            if (!drInfo.Exists)
            {
                drInfo.Create();
                return "Выполнено";
            }
            else
            {
                return "Не выполнено";
            }
        }
        public string DoResend(string passToSave, string FileToSend, string IP)
        {
            string ans = "";

            TcpFileClient tcpFileClient = new TcpFileClient(IP);
            tcpFileClient.SendFileRequest(passToSave + "|" + FileToSend);
            return ans;
        }
        public void SendFile(string filePath, NetworkStream stream)
        {
            if (!string.IsNullOrEmpty(filePath))
            { // файл есть, отдаём
                using (var fileIO = File.OpenRead(filePath))
                {
                    //stream.Write(BitConverter.GetBytes(fileIO.Length), 0, 8);
                    var b = BitConverter.GetBytes(fileIO.Length);                    
                    //fileIO.Length.ToString().Length
                    //stream.Write(b, 0, fileIO.Length.ToString().Length);
                    stream.Write(b, 0, b.Length);

                    var buffer = new byte[1024 * 8];
                    int count;

                    long allSize = fileIO.Length;
                    long howTransmit = 0;
                    while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, count);
                        howTransmit += count;
                        SendingEvent?.Invoke(howTransmit/allSize*100);
                    }
                }
            }
            else
            {// файл отсутствует 
                var data = BitConverter.GetBytes((int)RequestError.FileNotExist);
                stream.Write(data, 0, data.Length);
            }
        }

        public void SendFileFromMe(string filePath, NetworkStream stream)
        {
            try
            {
                var mess1 = "ready";
                var buf1 = Encoding.UTF8.GetBytes(mess1);
                stream.Write(buf1, 0, buf1.Length);

                Int64 bytesReceived = 0;
                int count;
                byte[] buffer = new byte[1024 * 8];
                stream.Read(buffer, 0, 1024);


                Int64 fileBytesSize = BitConverter.ToInt64(buffer, 0);

                using (var fileIO = File.Create(filePath))
                {

                    while (bytesReceived < fileBytesSize && (count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileIO.Write(buffer, 0, count);

                        bytesReceived += count;
                        double first = bytesReceived; double second = fileBytesSize;
                        //SendingEvent?.Invoke(first / second * 100);
                    }
                    var mess = "successfully";
                    var buf = Encoding.UTF8.GetBytes(mess);
                    stream.Write(buf, 0, buf.Length);
                }
            }
            catch
            {
                var mess = "unsuccessful";
                var buf = Encoding.UTF8.GetBytes(mess);
                stream.Write(buf, 0, buf.Length);
            }
        }
        public string GetDirectory(string directoryPass)
        {
            try
            {
                string ans = "";
                if (directoryPass == ".")
                {
                    var disks = DriveInfo.GetDrives();
                    foreach (var disk in disks)
                    {
                        ans += disk.Name + "\n";
                    }
                    return ans;
                }

                var direct = new DirectoryInfo(directoryPass);
                foreach (var dir in direct.GetDirectories())
                {
                    
                    ans += $"{dir.Name}|{dir.CreationTime.ToString()}|{-1}\n";
                }
                foreach (var file in direct.GetFiles())
                {
                    ans += $"{file.Name}|{file.CreationTime.ToString()}|{file.Length}\n";
                }                

                return ans;
            }
            catch
            {
                return RequestError.DirectoryNotExist.ToString();
            }
        }
    }
}
