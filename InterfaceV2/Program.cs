using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Interface
{
    class Program
    {
        static void Main(string[] args)
        {

            //Thread getConnectThread = new Thread(new ThreadStart(ConnectionV2.GetConnect));
            //getConnectThread.Start();
            //ConnectionV2.GetConnect();
            while (true)
            {
                if (!AvailableConection.IfWasEdited) continue;
                Console.WriteLine();
                var ls = AvailableConection.ReturnGroupList();
                foreach(var mem in ls)
                {
                    Console.WriteLine("connection from - " + mem);
                }
            }
        }
    }
}
