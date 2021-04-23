using Interface;
using InterfaceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary1;
using MyNetworkInterface;


namespace Share_Data_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            IP CurentPC = new IP();
            PreLoading(ref CurentPC);


            NetworkInterection networkInterection = new NetworkInterection();
            networkInterection.CurentPC = CurentPC;

            Thread sendingMessagesForBroadcastAddress = new Thread(new ThreadStart(networkInterection.SendBroadcastOfferToConnect));
            sendingMessagesForBroadcastAddress.IsBackground = true;
            sendingMessagesForBroadcastAddress.Start();


            //Thread sendThread = new Thread(new ThreadStart(ConnectionV2.SendBroadcastOfferToConnect)); //созадем новый поток для отправки сообщения на широковещательный
            //sendThread.IsBackground = true;
            //sendThread.Start(); // запускаем процесс отправки сообщений на широковещательный

            Thread DoRequestsRecieveingThread = new Thread(new ThreadStart(RequestInteractivity.DoRequestsRecieveing)); //созадем новый поток отдельно для получения
            DoRequestsRecieveingThread.IsBackground = true;
            DoRequestsRecieveingThread.Priority = ThreadPriority.Highest;
            DoRequestsRecieveingThread.Start();// Запускаем процесс получния запросов

            //Thread listenThread = new Thread(new ThreadStart(TcpServer.ListenRequest)); //созадем новый поток отдельно для получения сообщений            
            //listenThread.IsBackground = true;
            //listenThread.Start();

            InitializeComponent();
        }

        //NotUse
        #region ForImage

        private async void image_left_MouseEnter(object sender, MouseEventArgs e)
        {
            l = 1;
            await Task.Run(() => ScrollingList_l());
        }

        private async void image_right_MouseEnter(object sender, MouseEventArgs e)
        {
            r = 1;
            await Task.Run(() => ScrollingList_r());
        }
        private void image_left_MouseLeave(object sender, MouseEventArgs e)
        {
            l = 0;
        }
        private void image_right_MouseLeave(object sender, MouseEventArgs e)
        {
            r = 0;
        }


        int l = 0;
        int r = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">0 - влево, 1 - вправо</param>
        void ScrollingList_l()
        {
            while (l != 0)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    scroll.LineLeft();
                });
                Thread.Sleep(100);
            }
        }
        void ScrollingList_r()
        {
            while (r != 0)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    scroll.LineRight();
                });
                Thread.Sleep(100);
            }
        }

        #endregion

        private void PreLoading(ref IP CurentPC)
        {
            CurentPC.OnGettingCurrentHostName += HostNameWasGot;
            CurentPC.OnGettingIpAddressCurentPC += IpAddressWasGot;
            CurentPC.OnGettingSubnetMask += SubnetMaskWasGot;
            CurentPC.OnGettingBroadcastAddressCurentNetwork += BroadcastAddressCurentNetworkWasGot;

            CurentPC.GetCurrentHostNameASYNC();
        }


        private void HostNameWasGot(object sender, bool status)
        {
            IP CurentPC = (IP)sender;
            if(status==true)
            {
                CurentPC.GetIpAddressCurentPCASYNC();
            }
        }
        private void IpAddressWasGot(object sender, bool status)
        {
            IP CurentPC = (IP)sender;
            if (status == true)
            {
                CurentPC.GetSubnetMaskASYNC();
            }
        }
        private void SubnetMaskWasGot(object sender, bool status)
        {
            IP CurentPC = (IP)sender;
            if (status == true)
            {
                CurentPC.GetBroadcastAddressCurentNetworkASYNC();
            }
        }
        private void BroadcastAddressCurentNetworkWasGot(object sender, bool status)
        {
            bool dataInForm = false;
            IP CurentPC = (IP)sender;
            while (!dataInForm)
            {
                
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    if (MainLoadWindow.IsLoaded)
                    {

                        nameCurentPc.Content = CurentPC.ReturnNameInNetwork().ToString();
                        ipCurentPc.Content = CurentPC.ReturnIpAddress().ToString();
                        broadcastCurentPc.Content = CurentPC.ReturnBroadcastAddress().ToString();

                        dataInForm = true;
                    }
                });
                Thread.Sleep(100);
            }
            //Все определено для текущего пк

            // запускаем процесс отправки сообщений на широковещательный
            //networkInterection.SendBroadcastOfferToConnect();

            //IP CurentPC = (IP)sender;
            //if (status == true)
            //{
            //    CurentPC.GetSubnetMaskASYNC();
            //}
        }




        //This variable shows status search (false||true)/Эта переменная показывает статус поиска (незапущен||запущен)
        public bool status_search = false;
        private void searchingbrn_Click(object sender, RoutedEventArgs e)
        {
            if (status_search == false)
            {
                AvailableConection available = new AvailableConection();
                available.OnAddIpAdress += Draw;//Подписываемся на событие    


                Thread receiveThread = new Thread(new ParameterizedThreadStart(ConnectionV2.ReciveBroadcastOffer));
                receiveThread.IsBackground = true;
                receiveThread.Start(available);
                status_search = true;
            }

            Initializing_PC("denis", "127.0.0.1");

        }





        int count = 0;
        public void Draw(object sender, List<(string, string)> lst)
        {
            //_PCs.Add(new Data_about_PC { Adress = lst[count].Item2, Name = lst[count].Item1 });

            ////В переменной lst содержится лист с ip адресами
            ////Drawing_label(lst[count], x, y, this);
            ///

            Initializing_PC(lst[count].Item1, lst[count].Item2);

            ////Здесь производим расчет координат
            count++;
            //y += 20;

        }

        /// <summary>
        /// Initializing the found PC/Инициализация найденного компьютера
        /// </summary>
        /// <param name="name">Some name for PC/Какое-то имя компьютера</param>
        /// <param name="ip">IP address of this PC/IP адрес этого ПК</param>
        private void Initializing_PC(string name, string ip)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                UserControl1 userControl1 = new UserControl1();
                userControl1.Name = "Chel" + count;
                userControl1.namePc = name;
                userControl1.ipPc = ip;
                userControl1.widthForPicture = 120;
                userControl1.heightForPicture = 120;
                userControl1.maxheightForPictureh = 150;
                userControl1.maxwidthForPicture = 150;
                userControl1.widthAll = 220;
                userControl1.heightAll = 175;

                placeForPC.Children.Add(userControl1);  //Display the initialized PC fro screen
            });
        }

    }
}
