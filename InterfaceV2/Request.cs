using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace InterfaceV2
{
    public enum RequestTipe
    {
        Send = 1,
        GetDirectoryFiles,
        Download
    }
    public static class Request
    {
        public static void DoAfterSend(RequestTipe request)
        {

        }
        public static string ExecuteRecuest(string requestMessage, string aRequestSender)
        {
            string ans = "";

            string[] mes = requestMessage.Split(' ');
            int request = int.Parse(mes[1]);
            switch (request)
            {
                case (int)RequestTipe.GetDirectoryFiles:
                    ans = GetDirectory(mes[2]);
                    break;
                case (int)RequestTipe.Send:
                    break;
                case (int)RequestTipe.Download:
                    Download(mes[2]);
                    break;
                default:
                    Console.WriteLine("request isn't distingushed");
                    break;
            }

            return ans;
        }

        private static string Download(string filePass, string aRequestSender)
        {
            string ans = "";
            RequestInteractivity.SendRequst(aRequestSender, RequestTipe.Send, filePass);
            return ans;
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
                ans += file.Name + " ";
            }
            foreach (var dir in direct.GetDirectories())
            {
                ans += dir.Name + " ";
            }

            return ans;
        }
    }
}
