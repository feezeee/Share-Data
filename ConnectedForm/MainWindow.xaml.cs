using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using Microsoft.SqlServer.Server;

namespace ConnectedForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализируем двусвязанный список 
        /// </summary>
        DoublyLinkedList<string> linkedList0 = new DoublyLinkedList<string>();


        public MainWindow()
        {
            DataContext = this;
            setWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth/6-15;
            InitializeComponent();
        }

        /// <summary>
        /// Установка ip адреса 2-го пк в лабел 
        /// </summary>
        /// <param name="Ip">Передаем сам ip</param>
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

        #region загрузка информации в форму 
        public void loadInfromationAboutFiles0(string nameFile, string time, string sizeFile)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                files dataFile = new files() // создаём экземпляр класса        
                {
                    nameFile = nameFile, // указываем имя файла  
                    time = time, // указываем время создания    
                    sizeFile = sizeFile // указываем пароль  
                };
                listUsers0.Items.Add(dataFile); // выводим строку в список 
            });
        }

        public void loadInfromationAboutFiles1(string nameFile, string time, string sizeFile)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                files dataFile = new files() // создаём экземпляр класса        
                {
                    nameFile = nameFile, // указываем имя файла  
                    time = time, // указываем время создания    
                    sizeFile = sizeFile // указываем пароль  
                };
                listUsers1.Items.Add(dataFile); // выводим строку в список 
            });
        }
        #endregion



    /// <summary>
    /// Загружает файлы в форму по заданному пути
    /// </summary>
    /// <param name="sender">Указываем форму, в которой будем отрисовывать</param>
    /// <param name="ip">Указываем Ip адрес пк, у которого запрашивает директории по указанному пути</param>
    /// <param name="path">Указываем сам путь</param>
    /// <param name="control">Рисуем в первом контроле или во втором(по умолчанию - 0, (0/1))</param>
        public void loadFiles(object sender,string ip, string path,int control=0)
        {

                var myIp = ip;//ip текущего пк
            linkedList0.Add(path);
                var ans = RequestInteractivity.SendRequst(myIp, RequestTipe.GetDirectoryFiles, path);
                ans = ans.Remove(0, 7);
                var files = ans.Split('\n');
                files[files.Length - 1] = null;
                MainWindow mainWindow = (MainWindow)sender;
                if (control == 0)
                    this.Dispatcher.Invoke((ThreadStart)delegate
                    {
                       // Очищаем list
                            //************************
                            listUsers0.Items.Clear();
                    });
                else if (control == 1)
                    this.Dispatcher.Invoke((ThreadStart)delegate
                    {
                        // Очищаем list
                        //************************
                        listUsers1.Items.Clear();
                    });
                foreach (var file in files)
                {
                if (file != null)
                {
                    List<(string, string, string)> ps = mainWindow.CuttingMessages(file);
                    if (control == 0)
                    {
                        this.Dispatcher.Invoke((ThreadStart)delegate
                        {
                            pathbox0.IsEnabled = true;
                            mainWindow.loadInfromationAboutFiles0(ps[0].Item1, ps[0].Item2, ps[0].Item3);
                        });
                    }

                    else if (control == 1)
                    {
                        this.Dispatcher.Invoke((ThreadStart)delegate
                        {
                            pathbox1.IsEnabled = true;
                            mainWindow.loadInfromationAboutFiles1(ps[0].Item1, ps[0].Item2, ps[0].Item3);
                        });
                    }
                }
                else
                    linkedList0.Remove(path);
                }
                
        }



        //**********************************************************************************************
        //*********************************************************************************************
        //********************************************************************************************
        bool status = false;
        private void listUsers0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            string textbox = pathbox0.Text;
            if(list.SelectedItems.Count==1)
            {
                int index = list.SelectedIndex;//индекс нажатого итема
                //получаем имя папки/файла
                //**********************************************************
                object a = listUsers0.Items[index]; 
                var txt = a.GetType().GetProperty("nameFile").GetValue(a);
                //**********************************************************
                if ((pathbox0.Text == null || pathbox0.Text == "") && pathbox0.IsEnabled==true)
                    pathbox0.Text = txt.ToString();
                else if(pathbox0.IsEnabled==true)
                {
                    ok:
                    if (textbox[textbox.Length - 1] != '\\')
                    {
                        status = true;
                        textbox = textbox.Remove(textbox.Length - 1, 1);
                        goto ok;
                    }
                    if(status==true)
                    {
                        status = false;
                        pathbox0.Text = textbox;
                    }
                    else
                    pathbox0.Text += txt.ToString() + "\\";
                }
            }
        }

        private void listUsers1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {

                ListView list = (ListView)sender;
                string textbox = pathbox1.Text;
                if (list.SelectedItems.Count == 1)
                {
                    int index = list.SelectedIndex;//индекс нажатого итема
                                                   //получаем имя папки/файла
                                                   //**********************************************************
                    object a = listUsers1.Items[index];
                    var txt = a.GetType().GetProperty("nameFile").GetValue(a);
                    //**********************************************************
                    if ((pathbox1.Text == null || pathbox1.Text == "") && pathbox1.IsEnabled == true)
                        pathbox1.Text = txt.ToString();
                    else if (pathbox1.IsEnabled == true)
                    {
                        ok:
                        if (textbox[textbox.Length - 1] != '\\')
                        {
                            status = true;
                            textbox = textbox.Remove(textbox.Length - 1, 1);
                            goto ok;
                        }
                        if (status == true)
                        {
                            status = false;
                            pathbox1.Text = textbox;
                        }
                        else
                            pathbox1.Text += txt.ToString() + "\\";
                    }
                }
            });
        }

        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************


        //**********************************************************************************************
        //*********************************************************************************************
        //********************************************************************************************
        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
            string txt = pathbox0.Text;
            if (txt != "" && txt.Length > 3)
                if (txt[txt.Length - 1] == '\\'&& txt[txt.Length - 2]!='\\')
                {
                    //************************
                    txt =txt.Remove(txt.Length - 1,1);
                    pathbox0.IsEnabled = false;
                    await Task.Run(() => loadFiles(this,"127.0.0.1", txt,0));
                }
            if(txt!="" && txt.Length==3)
            {
                //************************
                pathbox0.IsEnabled = false;
                await Task.Run(() => loadFiles(this, "127.0.0.1", txt, 0));
            }
            if(txt=="")
            {
                //************************
                pathbox0.IsEnabled = false;
                await Task.Run(() => loadFiles(this, "127.0.0.1", ".",0));
            }    
        }
        private async void pathbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string ip = "";

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ip = LabelIp1.Content.ToString();
            });
                string txt = pathbox1.Text;
                if (txt != "" && txt.Length > 3)
                    if (txt[txt.Length - 1] == '\\' && txt[txt.Length - 2] != '\\')
                    {
                        //************************
                        txt = txt.Remove(txt.Length - 1, 1);
                        pathbox1.IsEnabled = false;
                        await Task.Run(() => loadFiles(this, ip, txt, 1));
                    }
                if (txt != "" && txt.Length == 3)
                {
                    //************************
                    pathbox1.IsEnabled = false;
                    await Task.Run(() => loadFiles(this,ip, txt, 1));
                }
                if (txt == "")
                {
                    //************************
                    pathbox1.IsEnabled = false;
                    await Task.Run(() => loadFiles(this,ip, ".", 1));
                }
            
        }

        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************


        //Сортирует сообщение(файл дата размер)
        public List<(string,string,string)> CuttingMessages(string message)
        {
            List<(string, string, string)> ps = new List<(string, string, string)>();
            int k = 0;
            string file = "", date = "",sizefile="";
            //ps[0].GetType().GetProperty("Item" + k)
            for (int i = 0; i < message.Length;i++)
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
            (string, string, string) member_str = (file, date,sizefile);
            ps.Add(member_str);
            return ps;  
        }

        private async void listUsers0_Drop(object sender, DragEventArgs e)
        {
            
            await Task.Run(() => ChekingAndLoadingFiles(listUsers0, listUsers1));
            
        }

        private async void listUsers1_Drop(object sender, DragEventArgs e)
        {     

            //object name = e.Data.GetData("System.Windows.Controls.SelectedItemCollection");

            await Task.Run(() => ChekingAndLoadingFiles(listUsers1,listUsers0));

            
        }

        List<files> _renamedFiles = new List<files>();
        /// <summary>
        /// Проверяет, есть ли совпадения в файлах
        /// </summary>
        /// <param name="control_in">Указывает, куда добавляем</param>
        /// <param name="control_from">указывает, откуда копируем</param>
        private void ChekingAndLoadingFiles(ListView control_in, ListView control_from)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {

                foreach (files files in control_from.SelectedItems)
                {
                    bool status = false;
                    if (files != null)
                    {
                        foreach(files files1 in control_in.Items)
                        {
                            if(files1.nameFile==files.nameFile)
                            {
                                status = true;
                                _renamedFiles.Add(files);
                                //AskedAboutRenamed(files);
                            }
                            //files1.nameFile
                        }
                        if (status != true)
                        {
                            //    ListViewItem listViewItem = new ListViewItem();
                            //    listViewItem.Content = files;
                            //    listViewItem.Background = new SolidColorBrush(Color.FromArgb(90, 90, 90, 45));

                            

                            control_in.Items.Add(files);
                        }
                    }
                }
            });
        }

        private string AskedAboutRenamed(files files)
        {
            return "";
        }


        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            listUsers0.AllowDrop = false;
            listUsers1.AllowDrop = true;
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(listUsers0, listUsers0.SelectedItems, DragDropEffects.Copy);
            }
        }

        private void ListViewItem_MouseMove_1(object sender, MouseEventArgs e)
        {
            listUsers0.AllowDrop = true;
            listUsers1.AllowDrop = false; 
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(listUsers1, listUsers1.SelectedItems, DragDropEffects.Copy);
            }
        }
    }
}
