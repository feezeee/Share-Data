using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interface;
using InterfaceV2;

namespace Share_Data
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread sendThread = new Thread(new ThreadStart(ConnectionV2.SendBroadcastOfferToConnect)); //созадем новый поток отдельно для получения
            sendThread.IsBackground = true;
            sendThread.Start(); // запускаем процесс отправки сообщений

            //Thread DoRequestsRecieveingThread = new Thread(new ThreadStart(RequestInteractivity.DoRequestsRecieveing)); //созадем новый поток отдельно для получения
            //DoRequestsRecieveingThread.IsBackground = true;
            //DoRequestsRecieveingThread.Start();// Запускаем процесс получния запросов

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
