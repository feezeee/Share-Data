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
        CreateDirrectory
    }
    public enum RequestError
    {
        FileNotExist = -1,
        DirectoryNotExist = -2,
        RequestIncomprehantable = -3
    }
    public static class Request
    {
        public static string DoSendingRequest(string requestMessage, NetworkStream stream = null)
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
        public static string ExecuteRecuest(string requestMessage, string IP = "0", NetworkStream stream = null)
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
                default:
                    System.Diagnostics.Debug.WriteLine("request isn't distingushed");
                    return RequestError.RequestIncomprehantable.ToString();
            }

            return ans;
        }

        private static string DoCreateDirrectory(string path)
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
        private static string DoResend(string passToSave, string FileToSend, string IP)
        {
            string ans = "";

            TcpFileClient tcpFileClient = new TcpFileClient(IP);
            tcpFileClient.SendFileRequest(FileToSend+"|"+passToSave);
            return ans;
        }
        private static void SendFile(string filePath, NetworkStream stream)
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

                    while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                        stream.Write(buffer, 0, count);
                }
            }
            else
            {// файл отсутствует 
                var data = BitConverter.GetBytes((int)RequestError.FileNotExist);
                stream.Write(data, 0, data.Length);
            }
        }
        private static string GetDirectory(string directoryPass)
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
                foreach (var file in direct.GetFiles())
                {
                    ans += $"{file.Name}|{file.LastWriteTime.ToString()}|{file.Length}\n";
                }
                foreach (var dir in direct.GetDirectories())
                {
                    ans += $"{dir.Name}|{dir.LastWriteTime.ToString()}|{-1}\n";
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
