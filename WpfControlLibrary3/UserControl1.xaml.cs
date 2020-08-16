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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfControlLibrary2;

namespace WpfControlLibrary3
{

    public class files
    {
        public string nameFile { get; set; }

        public string time { get; set; }

        public string sizeFile { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {       
        
        public UserControl1()
        {
            DataContext = this;
            IsHeightValue = 300;
            InitializeComponent();                       
        }

        private bool _status;

        public bool _Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _ip_from;

        public string Ip_From
        {
            get { return _ip_from; }
            set { _ip_from = value; }
        }

        private string _ip_to;

        public string Ip_To
        {
            get { return _ip_to; }
            set { _ip_to = value; }
        }

        private string _path_from;

        public string Path_From
        {
            get { return _path_from; }
            set { _path_from = value; }
        }


        private string _path_to;

        public string Path_To
        {
            get { return _path_to; }
            set { _path_to = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">путь по которому надо создать папку</param>
        public void CheckingDirectory(object path)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
            () =>
            
            {
                if (Ip_From == "Этот компьютер")
                {
                    var SetDirectory = RequestInteractivity.SendRequst(Ip_To, RequestTipe.CreateDirrectory, path.ToString());
                    if (SetDirectory == "Answer|Выполнено")
                    {
                        var ans = RequestInteractivity.SendRequst("127.0.0.1", RequestTipe.GetDirectoryFiles, Path_From); //Получить папки от удаленного пк (здесь от отправителя)
                        ans = ans.Remove(0, 7);
                        var files = ans.Split('\n');
                        files[files.Length - 1] = null;
                        foreach (var file in files)
                        {
                            if (file != null)
                            {
                                List<(string, string, string)> ps = CuttingMessages(file);
                                files files1 = new files() // создаём экземпляр класса        
                                {
                                    nameFile = ps[0].Item1, // указываем имя файла  
                                    time = ps[0].Item2, // указываем время создания    
                                    sizeFile = ps[0].Item3, // указываем пароль        
                                };
                                if (files1.sizeFile == "-1")
                                {
                                    // Если это папка
                                    WpfControlLibrary3.UserControl1 userControl1 = new WpfControlLibrary3.UserControl1();

                                    userControl1.Ip_From = Ip_From;
                                    userControl1.Ip_To = Ip_To;
                                    userControl1.Path_From = Path_From + "\\" + files1.nameFile;
                                    userControl1.Path_To = Path_To + "\\" + files1.nameFile;


                                    Thread receiveThread = new Thread(new ParameterizedThreadStart(userControl1.CheckingDirectory));
                                    receiveThread.IsBackground = true;
                                    receiveThread.Start(userControl1.Path_To);

                                    list_for_papki.Items.Add(userControl1);
                                }
                                else
                                {
                                    // Если это файл
                                    WpfControlLibrary2.UserControl1 flk = new WpfControlLibrary2.UserControl1();
                                    flk.Ip_From = Ip_From;
                                    flk.Ip_To = Ip_To;
                                    flk.Path_From = Path_From + "\\" + files1.nameFile;
                                    flk.Path_To = Path_To + "\\" + files1.nameFile;


                                    list_for_papki.Items.Add(flk);

                                    //var client = new TcpFileClient(Ip_From);

                                    ////Подписываемся на события
                                    //client.SendingEvent += flk.ChangedvalueForProgressBar;
                                    //client.FailEvent += flk.SendingFailMessage;
                                    //client.ReadyEvent += flk.SendingSuccessfullyMessage;

                                    //string _path = flk.Path_To + "|" + flk.Path_From;
                                    //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                                    //receiveThread.IsBackground = true;
                                    //receiveThread.Start(_path);

                                }

                            }
                        }
                    }
                    else
                    {
                        /// . . . 
                    }
                }
                else
                {
                    var SetDirectory = RequestInteractivity.SendRequst("127.0.0.1", RequestTipe.CreateDirrectory, path.ToString());
                    if (SetDirectory == "Answer|Выполнено")
                    {
                        var ans = RequestInteractivity.SendRequst(Ip_From, RequestTipe.GetDirectoryFiles, Path_From); //Получить папки от удаленного пк (здесь от отправителя)
                        ans = ans.Remove(0, 7);
                        var files = ans.Split('\n');
                        files[files.Length - 1] = null;
                        foreach (var file in files)
                        {
                            if (file != null)
                            {
                                List<(string, string, string)> ps = CuttingMessages(file);
                                files files1 = new files() // создаём экземпляр класса        
                                {
                                    nameFile = ps[0].Item1, // указываем имя файла  
                                    time = ps[0].Item2, // указываем время создания    
                                    sizeFile = ps[0].Item3, // указываем пароль        
                                };
                                if (files1.sizeFile == "-1")
                                {
                                    // Если это папка
                                    WpfControlLibrary3.UserControl1 userControl1 = new WpfControlLibrary3.UserControl1();

                                    userControl1.Ip_From = Ip_From;
                                    userControl1.Ip_To = Ip_To;
                                    userControl1.Path_From = Path_From + "\\" + files1.nameFile;
                                    userControl1.Path_To = Path_To + "\\" + files1.nameFile;


                                    Thread receiveThread = new Thread(new ParameterizedThreadStart(userControl1.CheckingDirectory));
                                    receiveThread.IsBackground = true;
                                    receiveThread.Start(userControl1.Path_To);

                                    list_for_papki.Items.Add(userControl1);
                                }
                                else
                                {
                                    // Если это файл
                                    WpfControlLibrary2.UserControl1 flk = new WpfControlLibrary2.UserControl1();
                                    flk.Ip_From = Ip_From;
                                    flk.Ip_To = Ip_To;
                                    flk.Path_From = Path_From + "\\" + files1.nameFile;
                                    flk.Path_To = Path_To + "\\" + files1.nameFile;


                                    list_for_papki.Items.Add(flk);

                                    //var client = new TcpFileClient(Ip_From);

                                    ////Подписываемся на события
                                    //client.SendingEvent += flk.ChangedvalueForProgressBar;
                                    //client.FailEvent += flk.SendingFailMessage;
                                    //client.ReadyEvent += flk.SendingSuccessfullyMessage;

                                    //string _path = flk.Path_To + "|" + flk.Path_From;
                                    //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                                    //receiveThread.IsBackground = true;
                                    //receiveThread.Start(_path);

                                }

                            }
                        }
                    }
                    else
                    {
                        /// . . . 
                    }
                }

            }));
        }

        //Сортирует сообщение(файл дата размер)
        private List<(string, string, string)> CuttingMessages(string message)
        {
            List<(string, string, string)> ps = new List<(string, string, string)>();
            int k = 0;
            string file = "", date = "", sizefile = "";
            //ps[0].GetType().GetProperty("Item" + k)
            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] != '|' && k == 0)
                    file += message[i];
                else if (message[i] != '|' && k == 1)
                    date += message[i];
                else if (message[i] != '|' && k == 2)
                    sizefile += message[i];
                else
                    k++;
            }
            (string, string, string) member_str = (file, date, sizefile);
            ps.Add(member_str);
            return ps;
        }

        public bool IsCheckBoxChecked_papka
        {
            get { return (bool)GetValue(IsCheckBoxChecked_papkaProperty); }
            set { SetValue(IsCheckBoxChecked_papkaProperty, value); }
        }
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckBoxChecked_papkaProperty =
            DependencyProperty.Register("IsCheckBoxChecked_papka", typeof(bool),
            typeof(System.Windows.Controls.UserControl), new UIPropertyMetadata(false));


        public int IsHeightValue
        {
            get { return (int)GetValue(IsHeightValueProperty); }
            set { SetValue(IsHeightValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHeightValueProperty =
            DependencyProperty.Register("IsHeightValue", typeof(int),
            typeof(System.Windows.Controls.UserControl), new UIPropertyMetadata(250));


        private void btn_checking_Click(object sender, RoutedEventArgs e)
        {
            if(IsCheckBoxChecked_papka == false)
            {
                IsCheckBoxChecked_papka = true;

                DoubleAnimation buttonAnimation = new DoubleAnimation();
                buttonAnimation.To = list_for_papki.Items.Count*26+50;
                buttonAnimation.Duration = TimeSpan.FromSeconds(0.3);
                grid_for_papki.BeginAnimation(Button.HeightProperty, buttonAnimation);
            }
            else
            {
                IsCheckBoxChecked_papka = false;

                DoubleAnimation buttonAnimation = new DoubleAnimation();
                buttonAnimation.To = 0;
                buttonAnimation.Duration = TimeSpan.FromSeconds(0.3);
                grid_for_papki.BeginAnimation(Button.HeightProperty, buttonAnimation);
            }    
        }

    }

}
