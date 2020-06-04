using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Share_Data
{
    class test
    {
        
        public void start()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            // **************************************************************************
            // БЕЗ МНОГОПОТОЧНОСТИ
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            // **************************************************************************
            for (int i = 208; i < 255; i++) // тут БЕЗ потока - с указанными границами IP-адресации (т.е. идет так - 10.0.21.161, 10.0.21.162, 10.0.21.163 и т.д.)
            {
                AutoResetEvent waiter = new AutoResetEvent(false);
                Ping pingSender = new Ping();
                pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);
                PingOptions options = new PingOptions(128, true);
                pingSender.Send("192.168.100." + i, timeout, buffer, options); // тут вы должны сами вставить свой вариант IP-адреса (не считая последней цифры после точки)
                waiter.WaitOne();
            }
            time.Stop();
            Console.WriteLine("Времы выполнения программы ДО в сек.: {0}", time.Elapsed.Seconds); // 21
            Console.WriteLine("Времы выполнения программы ДО в тиках: {0}\n", time.ElapsedTicks);
            Console.ReadLine();
            time.Restart();
            // **************************************************************************
            // МНОГОПОТОЧНОСТЬ
            Thread th = new Thread(new ThreadStart(Count));
            th.Start();
            th.Name = "th";
            Console.WriteLine("Имя потока: {0}", th.Name);
            Console.WriteLine("Запущен ли поток: {0}", th.IsAlive);
            th.Priority = ThreadPriority.Highest;
            Console.WriteLine("Приоритет потока: {0}", th.Priority);
            Console.WriteLine("Статус потока: {0}", th.ThreadState);
            Console.WriteLine("Домен приложения: {0}", Thread.GetDomain().FriendlyName);
            Console.WriteLine("\n");

            for (int i = 207; i <255; i++) // тут первая часть потока - с указанными границами IP-адресации (т.е. идет так - 10.0.21.0, 10.0.21.01, 10.0.21.02 и т.д.)
            {
                AutoResetEvent waiter = new AutoResetEvent(false);
                Ping pingSender = new Ping();
                pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);
                PingOptions options = new PingOptions(64, true);
                pingSender.SendAsync("192.168.100." + i, timeout, buffer, options, waiter); // тут вы должны сами вставить свой вариант IP-адреса (не считая последней цифры после точки)
                waiter.WaitOne();
            }
            Console.WriteLine("Статус потока: {0}\n", th.ThreadState);
            // **************************************************************************
            time.Stop();
            Console.WriteLine("Времы выполнения программы ПОСЛЕ в сек.: {0}", time.Elapsed.Seconds); // 21
            Console.WriteLine("Времы выполнения программы ПОСЛЕ в тиках: {0}\n", time.ElapsedTicks);
        }
        // **************************************************************************
        public static void Count()
        {
            for (int i = 10; i < 21; i++) // тут вторая часть потока - с указанными границами IP-адресации (т.е. идет так - 10.0.21.10, 10.0.21.11, 10.0.21.12 и т.д.)
            {
                AutoResetEvent waiter = new AutoResetEvent(false);
                Ping pingSender = new Ping();
                pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000;
                PingOptions options = new PingOptions(64, true);
                pingSender.SendAsync("10.0.21." + i, timeout, buffer, options, waiter); // тут вы должны сами вставить свой вариант IP-адреса (не считая последней цифры после точки)
                waiter.WaitOne();
            }
        }
        // **************************************************************************
        private static void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("Ping canceled.");
                ((AutoResetEvent)e.UserState).Set();
            }
            if (e.Error != null)
            {
                Console.WriteLine("Ping failed:");
                Console.WriteLine(e.Error.ToString());
                ((AutoResetEvent)e.UserState).Set();
            }
            PingReply reply = e.Reply;
            DisplayReply(reply);
            ((AutoResetEvent)e.UserState).Set();
        }

        public static void DisplayReply(PingReply reply)
        {
            if (reply == null)
                return;
            if (reply.Status != IPStatus.DestinationHostUnreachable && reply.Status != IPStatus.TimedOut)
            {
                Console.WriteLine("ping status: {0}", reply.Status);
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Address: {0}", reply.Address.ToString());
                    Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                    Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                    //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                    Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                    Console.WriteLine("\n\n");
                }
            }
        }
    }
}
