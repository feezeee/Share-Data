﻿using Interface;
using InterfaceV2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread sendThread = new Thread(new ThreadStart(ConnectionV2.SendBroadcastOfferToConnect)); //созадем новый поток отдельно для получения
            sendThread.IsBackground = true;
            //sendThread.Start(); // запускаем процесс отправки сообщений

            Thread DoRequestsRecieveingThread = new Thread(new ThreadStart(RequestInteractivity.DoRequestsRecieveing)); //созадем новый поток отдельно для получения
            DoRequestsRecieveingThread.IsBackground = true;
            DoRequestsRecieveingThread.Start();// Запускаем процесс получния запросов

            AvailableConection available = new AvailableConection();
            Thread receiveThread = new Thread(new ParameterizedThreadStart(ConnectionV2.ReciveBroadcastOffer));
            receiveThread.IsBackground = true;
            receiveThread.Start(available);

            Console.ReadLine();
            var availbCon = AvailableConection.ReturnGroupList();

            for (int i = 0; i < availbCon.Count; i++)
            {
                Console.WriteLine($"{i} - {availbCon[i].Item2}");
            }
            Console.WriteLine("enter the host number to interact");
            var p = int.Parse(Console.ReadLine());
            var reciverIP = availbCon[p].Item2;

            string pass = ".";
            while (true)
            {
                var ans = RequestInteractivity.SendRequst(reciverIP, RequestTipe.GetDirectoryFiles, pass);
                ans = ans.Remove(0, 7);
                var files = ans.Split(' ');

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
                string add = "";
                add = Console.ReadLine();
                if (pass[pass.Length - 1] == '.')
                {
                    pass = add;
                }
                else
                {
                    if (pass[pass.Length - 1] != '\\') pass += '\\';
                    pass += add;
                }

            }
        }
    }
}