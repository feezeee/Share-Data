using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace IPmanip
{
    public class CurrentIP
    {
        public CurrentIP()
        {
        }

        private static string broadIP;
        private static string localIP;
        public static string BroadIP
        {
            get
            {
                broadIP = extractBroadcastingAddress();
                return broadIP;
            }
        }
        public static string LocalIP
        {
            get
            {
                localIP = extractLocalAddress();
                return localIP;
            }
        }

        /// <summary>
        /// create a file is containing arp table
        /// </summary>
        /// <param name="fileName"></param>
        private static void CreateArpTable(string fileName) 
        {
            File.Delete("arp.bat");
            File.Delete("arp.txt");
            var arpBat = File.OpenWrite("arp.bat");
            byte[] array = System.Text.Encoding.Default.GetBytes($"chcp 861\narp.exe -a >{fileName}");
            arpBat.Write(array, 0, array.Length);
            arpBat.Close();
            var startInfo = new ProcessStartInfo();

            startInfo.FileName = "arp.bat";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var arpBatRun = Process.Start(startInfo);
            arpBatRun.WaitForExit();
        }
        private static string GetArpTable()
        {
            string fileName = "arp.txt";
            CreateArpTable(fileName);
            while (true)
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        var arpStream = new StreamReader(fileName); //открыть файл с таблицей для чтения
                        var table = arpStream.ReadToEnd();// прочитать arp nf,kbwe
                        arpStream.Close();
                        return table;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }   
        }

        private static string ExtractActiveInterface()
        {
            string interf = "none";
            try
            {
                string arpTab = GetArpTable() + "Interface:";

                var posFirstDin = arpTab.LastIndexOf("dynamic");

                var posEndInt = arpTab.IndexOf("Interface:", posFirstDin);
                var posSratIn = arpTab.LastIndexOf("Interface:", posFirstDin);
                interf = arpTab.Substring(posSratIn, posEndInt - posSratIn);
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return interf;
        }

        private static string extractBroadcastingAddress()
        {
            string address = "";
            string interf = ExtractActiveInterface();
            var vals = interf.Split(' ');
            for(int i = 0; i < vals.Length; i++)
            {
                if (vals[i] == "ff-ff-ff-ff-ff-ff")
                {
                    int j = i-1;
                    while(vals[j].Length < 4) j--;
                    address = vals[j];
                    break;
                }
            }
            return address;
        } 
        private static string extractLocalAddress()
        {
            string address = "";
            string interf = ExtractActiveInterface();
            
            var vals = interf.Split(' ');
            for(int i = 0; i < vals.Length; i++)
            {

                if (vals[i] == "Interface:")
                {
                    int j = i+1;
                    while(vals[j].Length < 4) j++;
                    address = vals[j];
                    break;
                }
            }
            return address;
        }
    }
}
