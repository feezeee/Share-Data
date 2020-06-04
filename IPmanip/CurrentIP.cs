using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

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

        public static void AssignCurrentLocalIP()
        {

        }
        public static void AssignCurrentBroadcastIP()
        {
            broadIP = extractBroadcastingAddress();
        }
        private static void CreateArpTable(string filename)
        {
            ProcessStartInfo psi = new ProcessStartInfo(); // новый процксс
            psi.CreateNoWindow = true; //скрыть 
            psi.FileName = "cmd"; // будет вызвана командна строка
            psi.Arguments = $@"/c arp -a > {filename}"; // команда записывающая arp таблицу в файл 
            Process.Start(psi); // выполнение команды
        }
        private static string GetArpTable()
        {
            string fileName = "arp.txt";
            CreateArpTable(fileName); // создаем arp таблицу в файле
            var arpStream = new StreamReader(fileName); //открыть файл с таблицей для чтения
            var table = arpStream.ReadToEnd();// прочитать arp nf,kbwe
            arpStream.Close();
            return table;
        }

        private static string ExtractActiveInterface()
        {
            string arpTab = GetArpTable() + "Interface:";
            int len = arpTab.Length;

            var posFirstDin = arpTab.IndexOf("dynamic");
            var posEndInt = arpTab.IndexOf("Interface:", posFirstDin);
            var posSratIn = arpTab.LastIndexOf("Interface:", posFirstDin);
            string interf = arpTab.Substring(posSratIn, posEndInt - posSratIn);
            return interf;
        }

        private static string extractBroadcastingAddress()
        {
            string address = "";
            string interf = ExtractActiveInterface();
            
            //interf = interf.Replace('\n', ' ');
            //interf = interf.Replace('\t', ' ');
            var vals = interf.Split(' ');
            for(int i = 0; i < vals.Length; i++)
            {
                
                //Console.WriteLine(vals[i]);
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
