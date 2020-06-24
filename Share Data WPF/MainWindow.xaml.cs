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

namespace Share_Data_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            Thread sendThread = new Thread(new ThreadStart(ConnectionV2.SendBroadcastOfferToConnect)); //созадем новый поток отдельно для получения
            sendThread.IsBackground = true;
            sendThread.Start(); // запускаем процесс отправки сообщений

            Thread DoRequestsRecieveingThread = new Thread(new ThreadStart(RequestInteractivity.DoRequestsRecieveing)); //созадем новый поток отдельно для получения
            DoRequestsRecieveingThread.IsBackground = true;
            //DoRequestsRecieveingThread.Start();// Запускаем процесс получния запросов

            InitializeComponent();
        }

        private void image_left_MouseEnter(object sender, MouseEventArgs e)
        {
            scroll.LineLeft();
        }

        private void image_right_MouseEnter(object sender, MouseEventArgs e)
        {
            scroll.LineRight();
        }

        public bool Status = false;
        private void searchingbrn_Click(object sender, RoutedEventArgs e)
        {
            if (Status == false)
            {
                AvailableConection available = new AvailableConection();
                available.onAddIpAdress += Draw;//Подписываемся на событие    


                Thread receiveThread = new Thread(new ParameterizedThreadStart(ConnectionV2.ReciveBroadcastOffer));
                receiveThread.IsBackground = true;
                receiveThread.Start(available);
                Status = true;
            }
            Drawing_picture_for_pc("denis", "192.168.100.100");

        }
        int count = 0;
        public void Draw(object sender, List<(string, string)> lst)
        {
            //_PCs.Add(new Data_about_PC { Adress = lst[count].Item2, Name = lst[count].Item1 });

            ////В переменной lst содержится лист с ip адресами
            ////Drawing_label(lst[count], x, y, this);
            Drawing_picture_for_pc(lst[count].Item1, lst[count].Item2);

            ////Здесь производим расчет координат
            count++;
            //y += 20;

        }
        private void Drawing_picture_for_pc(string name, string ip)
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
                stackpanel.Children.Add(userControl1);
            });
            //UserControl1 userControl1 = new UserControl1();
            //userControl1.namePc = name;
            //userControl1.ipPc = ip;
            //userControl1.widthForPicture = 120;
            //userControl1.heightForPicture = 120;
            //userControl1.maxheightForPictureh = 150;
            //userControl1.maxwidthForPicture = 150;
            //userControl1.widthAll = 220;
            //userControl1.heightAll = 175;
            //stackpanel.Children.Add(userControl1);
        }
    }
}
