using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IPmanip
{
    class Program
    {
        static void Main()
        {
            //CurrentIP currentIP = new CurrentIP();
            foreach(var e in CurrentIP.LocalIP)
                Console.WriteLine(e.BroadIP);
            Console.ReadLine();
        }
    }
}
