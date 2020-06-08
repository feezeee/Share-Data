using InterfaceV2;
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

            Thread getConnectThread = new Thread(new ThreadStart(RequestInteractivity.DoRequestsRecieveing));
            getConnectThread.Start();

            RequestInteractivity.SendRequst("127.0.0.1");
        }
    }
}
