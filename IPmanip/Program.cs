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
            var localAddr = IP.GetLocalIPAddress();

            Console.WriteLine("local addres is - {0}", localAddr);
        }
    }
}
