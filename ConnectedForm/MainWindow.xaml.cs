﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using interactDomain;
using InterfaceV2;

namespace ConnectedForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            setWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth/6-15;
            InitializeComponent();
        }

        public void setIp(object Ip)
        {
            LabelIp1.Content = Ip.ToString();
        }

        public class files
        {
            public string nameFile { get; set; }

            public string time { get; set; }

            public string sizeFile { get; set; }
        }

        private int _width;

        public int setWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;

        public int setHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        public void loadInfromationAboutFiles0(string nameFile, string time, string sizeFile)
        {
            files dataFile = new files() // создаём экземпляр класса        
            {
                nameFile = nameFile, // указываем имя файла  
                time = time, // указываем время создания    
                sizeFile = sizeFile // указываем пароль  
            };
            listUsers0.Items.Add(dataFile); // выводим строку в список 
        }

        public void loadInfromationAboutFiles1(string nameFile, string time, string sizeFile)
        {
            files dataFile = new files() // создаём экземпляр класса        
            {
                nameFile = nameFile, // указываем имя файла  
                time = time, // указываем время создания    
                sizeFile = sizeFile // указываем пароль  
            };
            listUsers1.Items.Add(dataFile); // выводим строку в список 
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void listUsers0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            if(list.SelectedItems.Count==1)
            {
                int index = list.SelectedIndex;
                object a = listUsers0.Items[index];
                var txt = a.GetType().GetProperty("nameFile").GetValue(a);
                listUsers0.Items.Clear();

                string pass = txt.ToString();
                var myIp = "127.0.0.1";
                var ans = RequestInteractivity.SendRequst(myIp, RequestTipe.GetDirectoryFiles, pass);
                ans = ans.Remove(0, 7);
                var files = ans.Split('\n');
                files[files.Length - 1] = null;

                foreach (var file in files)
                {
                    if (file != null)
                        loadInfromationAboutFiles0(file.ToString(), "", "");
                }
                string add = "";
                add = Console.ReadLine();
                if (pass[pass.Length - 1] == '.')
                {
                    pass = add;
                }
                else
                {
                    if (pass[pass.Length - 1] != '\\') pass += '\\';
                    pass += add;
                }
            }
        }

        private void listUsers1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            if (list.SelectedItems.Count == 1)
            {
                int index = list.SelectedIndex;
                object a = listUsers1.Items[index];
                var file = a.GetType().GetProperty("nameFile").GetValue(a);
            }
        }
    }
}
