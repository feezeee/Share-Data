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
            Console.WriteLine(CurrentIP.LocalIP[0].LocalIP);
            Console.ReadLine();
        }
    }
}
