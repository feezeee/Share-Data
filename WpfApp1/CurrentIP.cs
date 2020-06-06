﻿using System;
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
        /// <param name="filename"></param>
        private static void CreateArpTable(string filename) 
        {
            var arpBat = File.OpenWrite("arp.bat");
            byte[] array = System.Text.Encoding.Default.GetBytes($"chcp 861\narp.exe -a > {filename}");
            arpBat.Write(array, 0, array.Length);
            arpBat.Close();
            Process.Start("arp.bat");
            while (!File.Exists(filename));
        }
        private static string GetArpTable()
        {
            string fileName = "arp.txt";
            //File.Create("arp.txt");
            CreateArpTable(fileName); //создаем arp таблицу в файле
            var arpStream = new StreamReader(fileName); //открыть файл с таблицей для чтения
            var table = arpStream.ReadToEnd();// прочитать arp nf,kbwe
            arpStream.Close();
            return table;
        }

        private static string ExtractActiveInterface()
        {
            string arpTab = GetArpTable() + "Interface:";
            int len = arpTab.Length;

            var posFirstDin = arpTab.LastIndexOf("dynamic");

            var posEndInt = arpTab.IndexOf("Interface:", posFirstDin);
            var posSratIn = arpTab.LastIndexOf("Interface:", posFirstDin);
            string interf = arpTab.Substring(posSratIn, posEndInt - posSratIn);
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
