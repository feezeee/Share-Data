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
            Console.WriteLine($"широковещательный адресс в локальной сети - {CurrentIP.BroadIP}");
            Console.WriteLine($"локальный адресс в локальной сети - {CurrentIP.LocalIP}");
        }
    }
}
