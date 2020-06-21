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
    }
    public static class Request
    {
        public static string DoSendingRequest(string requestMessage, NetworkStream stream = null)
        {
            string ans = "";

            string[] mes = requestMessage.Split(' ');
            int request = int.Parse(mes[1]);
            switch (request)
            {
                case (int)RequestTipe.GetDirectoryFiles:
                    ans = GetDirectory(mes[2]);
                    break;
                case (int)RequestTipe.SendFile:
                    SendFile(mes[2], stream);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("request isn't distingushed");
                    break;
            }

            return ans;
        }
        public static string ExecuteRecuest(string requestMessage, NetworkStream stream = null)
        {
            string ans = "";

            string[] mes = requestMessage.Split(' ');
            int request = int.Parse(mes[1]);
            switch (request)
            {
                case (int)RequestTipe.GetDirectoryFiles:
                    ans = GetDirectory(mes[2]);
                    break;
                case (int)RequestTipe.SendFile:
                    SendFile(mes[2], stream);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("request isn't distingushed");
                    break;
            }

            return ans;
        }
        private static void SendFile(string filePath, NetworkStream stream)
        {
            if (!string.IsNullOrEmpty(filePath))
            { // файл есть, отдаём
                using (var fileIO = File.OpenRead(filePath))
                {
                    //stream.Write(BitConverter.GetBytes(fileIO.Length), 0, 8);
                    stream.Write(BitConverter.GetBytes(fileIO.Length), 0, fileIO.Length.ToString().Length);

                    var buffer = new byte[1024 * 8];
                    int count;

                    while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                        stream.Write(buffer, 0, count);
                }
            }
        }

        private static string GetDirectory(string directoryPass)
        {
            string ans = "";
            if(directoryPass == ".")
            {
                var disks = DriveInfo.GetDrives();
                foreach(var disk in disks)
                {
                    ans += disk.Name + " ";
                }
                return ans;
            }
            
            var direct = new DirectoryInfo(directoryPass);
            foreach (var file in direct.GetFiles())
            {
                ans += $"{file.Name} {file.LastWriteTime.ToString()} {file.Length}\n";
            }
            foreach (var dir in direct.GetDirectories())
            {
                ans += $"{dir.Name} {dir.LastWriteTime.ToString()} {-1}\n";
            }

            return ans;
        }
    }
}
