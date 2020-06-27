﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Runtime.CompilerServices;
using ConnectedForm;
using InterfaceV2;
using System.Threading;
using Interface;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            //this.DataContext = WidthForPicture;
            DataContext = this;
        }


        private string _name;
        public string namePc
        {
            get { return this._name; }
            set { this._name = value; }
        }


        private string _ip;
        public string ipPc
        {
            get { return this._ip; }
            set { this._ip = value; }
        }

        private string status;
        public string statusForMouse
        {
            get { return this.status; }
            set { this.status = value; }
        }

        private int width;
        public int widthForPicture
        {
            get { return this.width; }
            set { width = value; }
        }

        private int height;
        public int heightForPicture
        {
            get { return this.height; }
            set { height = value; }
        }

        private int maxwidth;
        public int maxwidthForPicture
        {
            get { return this.maxwidth; }
            set { maxwidth = value; }
        }

        private int maxheight;
        public int maxheightForPictureh
        {
            get { return this.maxheight; }
            set { maxheight = value; }
        }

        private int _widthAll;
        public int widthAll
        {
            get { return this._widthAll; }
            set { _widthAll = value; }
        }

        private int _heightAll;
        public int heightAll
        {
            get { return this._heightAll; }
            set { _heightAll = value; }
        }

        private void imagePc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            string txt;
            Image img = (Image)sender;
            var obj = img.Parent;
            Grid grid = (Grid)obj;
            /////////////////////////
            string ip="", text="";//ip и имя второго пк
            /////////////////////////
            foreach (var label in VisualHelper.FindVisualChildren<Label>(grid))
            {
                if(label.Name=="label_text")
                {
                    text = label.Content.ToString();
                }
                if (label.Name == "label_ip")
                {
                    ip = label.Content.ToString();
                }
            }

            var myIp = "127.0.0.1";
            string pass = ".";
            //*********************************
            var nextIp = ip;
            pass = ".";
            loadform(myIp, ip, "", ".");
        }
        public void loadform(string ip,string nextip,string text,string param)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.setIp(nextip);
            mainWindow.Show();
            mainWindow.loadFiles(mainWindow,ip, param, 0);
            mainWindow.loadFiles(mainWindow,nextip, param, 1);

        }
        public class VisualHelper
        {
            public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
            {
                if (depObj != null)
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                    {
                        DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                        if (child != null && child is T)
                        {
                            yield return (T)child;
                        }

                        foreach (T childOfChild in FindVisualChildren<T>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }
    }

}
