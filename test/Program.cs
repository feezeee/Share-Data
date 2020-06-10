using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread sendThread = new Thread(new ThreadStart(ConnectionV2.SendBroadcastOfferToConnect)); //созадем новый поток отдельно для получения
            sendThread.IsBackground = true;
            sendThread.Start(); // запускаем процесс отправки сообщений
        }
    }
}
