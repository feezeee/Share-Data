using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace IPmanip
{
    
    public class Local
    {
        public Local(string localIP, string broadIP)
        {
            LocalIP = localIP;
            BroadIP = broadIP;
        }
        public string LocalIP = "";
        public string BroadIP = "";
    }
    public class CurrentIP
    {
        static object locker = new object();
        public CurrentIP()
        {
        }

        private static List<Local> _localIP = null;


        public static List<Local> LocalIP
        {
            
            get
            {
                lock(locker)
                {
                    if (_localIP == null) _localIP = GetAddresses();
                    return _localIP;
                }   
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

        private static string Extractintarface(string arpTab)
        {
            try
            {
                string interf = "";
                var posFirstDin = arpTab.LastIndexOf("dynamic");
                var posEndInt = arpTab.IndexOf("Interface:", posFirstDin);
                var posSratIn = arpTab.LastIndexOf("Interface:", posFirstDin);
                interf = arpTab.Substring(posSratIn, posEndInt - posSratIn);

                return interf;
            }
            catch
            {
                return "";
            }
        }
        private static List<string> ExtractInterfaces()
        {
            var interfaces = new List<string>();
            try
            {
                string arpTab = GetArpTable() + "Interface:";
               
                while (true)
                {
                    string interf = Extractintarface(arpTab);
                    if (interf == "") break;
                    Console.WriteLine(arpTab);
                    arpTab = arpTab.Remove(arpTab.IndexOf(interf), interf.Length);
                    interfaces.Add(interf);
                }     

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return interfaces;
        }
        private static string ExtractLocalAddress(string interf)
        {
            string address = "";

            var vals = interf.Split(' ');
            for (int i = 0; i < vals.Length; i++)
            {

                if (vals[i] == "Interface:")
                {
                    int j = i + 1;
                    while (vals[j].Length < 4) j++;
                    address = vals[j];
                    break;
                }
            }
            return address;
        }
        private static string ExtractBroadcastingAddress(string interf)
        {
            var address = "";
            var vals = interf.Split(' ');
            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i] == "ff-ff-ff-ff-ff-ff")
                {
                    int j = i - 1;
                    while (vals[j].Length < 4) j--;
                    address = vals[j];
                    break;
                }
            }
            if ("255.255.255.255" == address) address = "";
            return address;
        }
        private static List<Local> GetAddresses()
        {
            var addresses = new List<Local>();
            var interfaces = ExtractInterfaces();
            foreach(var interf in interfaces)
            {
                var BroadAddress = ExtractBroadcastingAddress(interf);
                var LocalAddress = ExtractLocalAddress(interf);
                if (BroadAddress == "") continue;
                addresses.Add(new Local(LocalAddress, BroadAddress));
            }
            return addresses;
        } 
        
    }
}
