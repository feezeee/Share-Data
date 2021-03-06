﻿using InterfaceV2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WpfControlLibrary2
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
    public partial class UserControl1 : System.Windows.Controls.UserControl
    {
        public files files_inf = new files();
        public UserControl1()
        {

            InitializeComponent();
            DataContext = this;

            //Thread myThread = new Thread(new ThreadStart(CheckingStatus_progress));
            //myThread.Start(); // запускаем поток
        }

        private double _widthValue;

        public double _WidthValue
        {
            get { return _widthValue; }
            set { _widthValue = value; }
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

        private string _name;

        public string Name_
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _namefile;

        public string NameFile
        {
            get { return _namefile; }
            set { _namefile = value; }
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

        public bool IsCheckBoxChecked
        {
            get { return (bool)GetValue(IsCheckBoxCheckedProperty); }
            set { SetValue(IsCheckBoxCheckedProperty, value); }
        }
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckBoxCheckedProperty =
            DependencyProperty.Register("IsCheckBoxChecked", typeof(bool),
            typeof(System.Windows.Controls.UserControl), new UIPropertyMetadata(false));

        public void ChangedvalueForProgressBar(double value)
        {            
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if (label_status.Content.ToString() == "Ожидание")
                {
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding();
                    binding.Source = status_progress;
                    binding.Path = new PropertyPath("Value");
                    label_status.SetBinding(System.Windows.Controls.Label.ContentProperty, binding);

                }
                double valuse = Math.Round(value, 2);
                if (valuse == 100)
                    valuse = 99.9;
                status_progress.Value = valuse;

            });
        }

        public void SendingFailMessage()
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                label_status.Content = "Ошибка!";
            });
        }

        public delegate void MethodContainer();

        //Событие OnCount c типом делегата  OncompleteList.
        public event MethodContainer OnCompleteTransmit;

        public void SendingSuccessfullyMessage()
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                label_status.Content = "Отправлено";
                OnCompleteTransmit?.Invoke();
            });
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //OnCompleteTransmit?.Invoke();
            if (IsCheckBoxChecked == false)
            {
                IsCheckBoxChecked = true;

            }
            else
            {
                IsCheckBoxChecked = false;
                //ImageSource imageSource1 = new BitmapImage(new Uri("E:\\Share Data\\Share-Data\\WpfControlLibrary2\\2.jpg"));
                //imageforbtn.ImageSource = imageSource1;

            }

        }
    }

}
