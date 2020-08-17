﻿using InterfaceV2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfControlLibrary2;

namespace ConnectedForm
{

    public class files
    {
        public string nameFile { get; set; }

        public string time { get; set; }

        public string sizeFile { get; set; }

    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализируем двусвязанный список 
        /// </summary>
        DoublyLinkedList<string> linkedList0 = new DoublyLinkedList<string>();

        public bool IsPressedBtn = true;

        public MainWindow()
        {
            OncompleteList += StartedAdding;
            DataContext = this;
            setWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth / 6 - 15;
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
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            () =>
            {
                string nameSize = "b";

                if (sizeFile == "-1")
                {
                    sizeFile = "";
                }
                else if (sizeFile == "")
                {

                }
                else if (Convert.ToDouble(sizeFile) > 1024)
                {
                    sizeFile = Math.Round((Convert.ToDouble(sizeFile) / 1024), 2).ToString();
                    nameSize = "Kb";
                    if (Convert.ToDouble(sizeFile) > 1024)
                    {
                        sizeFile = Math.Round((Convert.ToDouble(sizeFile) / 1024), 2).ToString();
                        nameSize = "Mb";
                        if (Convert.ToDouble(sizeFile) > 1024)
                        {
                            sizeFile = Math.Round((Convert.ToDouble(sizeFile) / 1024), 2).ToString();
                            nameSize = "Gb";
                        }
                    }
                    sizeFile += " " + nameSize;
                }
                else
                    sizeFile += " " + nameSize;
                files dataFile = new files() // создаём экземпляр класса        
                {
                    nameFile = nameFile, // указываем имя файла  
                    time = time, // указываем время создания    
                    sizeFile = sizeFile, // указываем пароль        
                };
                listUsers0.Items.Add(dataFile); // выводим строку в список 
            }));
        }

        public void loadInfromationAboutFiles1(string nameFile, string time, string sizeFile)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            () =>
            {
                string nameSize = "b";

                if (sizeFile == "-1")
                {
                    sizeFile = "";
                }
                else if (sizeFile == "")
                {

                }
                else if (Convert.ToDouble(sizeFile) > 1024)
                {
                    sizeFile = Math.Round((Convert.ToDouble(sizeFile) / 1024),2).ToString();
                    nameSize = "Kb";
                    if (Convert.ToDouble(sizeFile) > 1024)
                    {
                        sizeFile = Math.Round((Convert.ToDouble(sizeFile) / 1024),2).ToString();
                        nameSize = "Mb";
                        if (Convert.ToDouble(sizeFile) > 1024)
                        {
                            sizeFile = Math.Round((Convert.ToDouble(sizeFile) / 1024),2).ToString();
                            nameSize = "Gb";
                        }
                    }
                    sizeFile += " " + nameSize;
                }
                else
                    sizeFile += " " + nameSize;
                files dataFile = new files() // создаём экземпляр класса        
                {
                    nameFile = nameFile, // указываем имя файла  
                    time = time, // указываем время создания   
                    sizeFile = sizeFile, // указываем пароль 
                };
                listUsers1.Items.Add(dataFile);                // выводим строку в список
            }));
        }
        #endregion



        /// <summary>
        /// Загружает файлы в форму по заданному пути
        /// </summary>
        /// <param name="sender">Указываем форму, в которой будем отрисовывать</param>
        /// <param name="ip">Указываем Ip адрес пк, у которого запрашивает директории по указанному пути</param>
        /// <param name="path">Указываем сам путь</param>
        /// <param name="control">Рисуем в первом контроле или во втором(по умолчанию - 0, (0/1))</param>
        public void loadFiles(object sender, string ip, string path, int control = 0)
        {

            var myIp = ip;//ip текущего пк
            linkedList0.Add(path);
            var ans = RequestInteractivity.SendRequst(myIp, RequestTipe.GetDirectoryFiles, path);
            ans = ans.Remove(0, 7);
            var files = ans.Split('\n');
            files[files.Length - 1] = null;
            MainWindow mainWindow = (MainWindow)sender;
            if (control == 0)
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                () =>
                {
                // Очищаем list
                //************************
                listUsers0.Items.Clear();
                }));
            else if (control == 1)
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                    () =>
                    {
            
                    // Очищаем list
                    //************************
                    listUsers1.Items.Clear();
                    }));
            foreach (var file in files)
            {
                if (file != null)
                {
                    List<(string, string, string)> ps = mainWindow.CuttingMessages(file);
                    if (control == 0)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () =>
                        {     
                            mainWindow.loadInfromationAboutFiles0(ps[0].Item1, ps[0].Item2, ps[0].Item3);
                        }));
                    }

                    else if (control == 1)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () =>
                        {
                             mainWindow.loadInfromationAboutFiles1(ps[0].Item1, ps[0].Item2, ps[0].Item3);
                        }));
                    }
                }
                else
                    linkedList0.Remove(path);
            }
            if(control == 0)
                    {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                () =>
                {
                pathbox0.IsEnabled = true;
                    
                }));
            }
            else if (control == 1)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                () =>
                {
                pathbox1.IsEnabled = true;
                    
                }));
            }
        }



        //**********************************************************************************************
        //*********************************************************************************************
        //********************************************************************************************
        bool status = false;
        private void listUsers0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            () =>
            {
                ListView list = (ListView)sender;
                string textbox = pathbox0.Text;
                if (list.SelectedItems.Count == 1)
                {
                    int index = list.SelectedIndex;//индекс нажатого итема
                                                   //получаем имя папки/файла
                                                   //**********************************************************
                    object a = listUsers0.Items[index];
                    var txt = a.GetType().GetProperty("nameFile").GetValue(a);
                    //**********************************************************
                    if ((pathbox0.Text == null || pathbox0.Text == "") && pathbox0.IsEnabled == true)
                        pathbox0.Text = txt.ToString();
                    else if (pathbox0.IsEnabled == true)
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
                            pathbox0.Text = textbox;
                        }
                        else
                            pathbox0.Text += txt.ToString() + "\\";
                    }
                }
            }));
        }

        private void listUsers1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            () =>
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
            }));
        }

        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************


        //**********************************************************************************************
        //*********************************************************************************************
        //********************************************************************************************
       
        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = "";
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            () =>
            {
                txt = pathbox0.Text;
            }));
            if (txt != "" && txt.Length > 3)
                if (txt[txt.Length - 1] == '\\' && txt[txt.Length - 2] != '\\')
                {
                    //************************
                    txt = txt.Remove(txt.Length - 1, 1);
                    pathbox0.IsEnabled = false;                   
                    await Task.Run(() => loadFiles(this, "127.0.0.1", txt, 0));
                }
            if (txt != "" && txt.Length == 3)
            {
                //************************
                pathbox0.IsEnabled = false;                
                await Task.Run(() => loadFiles(this, "127.0.0.1", txt, 0));
            }
            if (txt == "")
            {
                //************************
                pathbox0.IsEnabled = false;
                await Task.Run(() => loadFiles(this, "127.0.0.1", ".", 0));
            }
        }
        private async void pathbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string ip = "";

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            () =>
            {
                ip = LabelIp1.Content.ToString();
            }));
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
                await Task.Run(() => loadFiles(this, ip, txt, 1));
            }
            if (txt == "")
            {
                //************************
                pathbox1.IsEnabled = false;
                await Task.Run(() => loadFiles(this, ip, ".", 1));
            }

        }

        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************


        //Сортирует сообщение(файл дата размер)
        public List<(string, string, string)> CuttingMessages(string message)
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



        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************
        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************
        //********************************************************************************************
        //*********************************************************************************************
        //**********************************************************************************************

        private async void listUsers0_Drop(object sender, DragEventArgs e)
        {
            if (pathbox0.IsEnabled == true)
            {
                await Task.Run(() => ChekingAndLoadingFiles
                (
                    listUsers0, listUsers1, pathbox1, pathbox0, LabelIp1, LabelIp0
                ));
            }

        }


        private async void listUsers1_Drop(object sender, DragEventArgs e)
        {

            //object name = e.Data.GetData("System.Windows.Controls.SelectedItemCollection");\
            if(pathbox1.IsEnabled==true)
            {

                await Task.Run(() => ChekingAndLoadingFiles
                (
                    listUsers1, listUsers0, pathbox0, pathbox1, LabelIp0, LabelIp1
                ));

            }

        }

        //Лист, в который заносится информация о совпадающих файлах
        List<files> _renamedFiles = new List<files>();

        //Лист, в который заносится информация о нуждающихся в добалении файлов        
        List<(files,string, string, string, string, string)> _neededToAdding = new List<(files,string, string, string, string, string)>();



        /// <summary>
        /// Проверяет, есть ли совпадения в файлах
        /// </summary>
        /// <param name="control_in">Указывает, куда добавляем</param>
        /// <param name="control_from">указывает, откуда копируем</param>
        private void ChekingAndLoadingFiles(ListView control_in, ListView control_from, TextBox from, TextBox to, Label ip_from,Label ip_to)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                () =>
                {

                    foreach (files files in control_from.SelectedItems)
                    {
                        bool status = false;
                        if (files != null)
                        {
                            foreach (files files1 in control_in.Items)
                            {
                                if (files1 != null)
                                    if (files1.nameFile == files.nameFile)
                                    {
                                        status = true;
                                        _renamedFiles.Add(files);
                                        //AskedAboutRenamed(files);
                                    }
                                //files1.nameFile
                            }
                            if (status != true)
                            {
                            (files, string, string, string, string, string) member = (files, from.Text, to.Text, files.nameFile, ip_from.Content.ToString(), ip_to.Content.ToString());
                                if(from.Text!="" &&to.Text!="")
                                _neededToAdding.Add(member);

                            }
                        }
                    }
                }));
                OncompleteList();
            }
            catch
            { }

        }

        public delegate void MethodContainer();

        //Событие OnCount c типом делегата  OncompleteList.
        public event MethodContainer OncompleteList;


        private void StartedAdding()
        {
            Thread thread1 = new Thread(AddingFiles);
            thread1.Start();
        }

        private void AddingFiles()
        {
            for (int i = 0; i < _neededToAdding.Count; i++)
            {

                
                    string from = _neededToAdding[i].Item2;
                    string to = _neededToAdding[i].Item3;
                    while (true)
                    {
                        if (from[from.Length - 1] != '\\')
                            from = from.Remove(from.Length - 1);
                        else
                            break;
                    }
                    while (true)
                    {
                        if (to[to.Length - 1] != '\\')
                            to = to.Remove(to.Length - 1);
                        else
                            break;
                    }

                    (object, string, string, System.Drawing.Image, string, string) obj = (_neededToAdding[i].Item1, from + _neededToAdding[i].Item4, to, null, _neededToAdding[i].Item5, _neededToAdding[i].Item6);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                () =>
                {
                    Thread receiveThread = new Thread(new ParameterizedThreadStart(AddingInDonwloadList));
                    receiveThread.IsBackground = true;
                    receiveThread.Start(obj);

                    //AddingInDonwloadList(_neededToAdding[i].Item1, from + _neededToAdding[i].Item4, to, null,_neededToAdding[i].Item5, _neededToAdding[i].Item6);
                    //ListView _from = _neededToAdding[i].Item2;//from
                    //ListView _in = _neededToAdding[i].Item3;
                    ////Запускаем функцию отправки
                    //if (_neededToAdding[i].Item5 != _neededToAdding[i].Item8.Text)
                    //    _in.Items.Add(_neededToAdding[i].Item1);
                    //Запускаем функцию отправки
                    //НЕ ГОТОВОООО!!!!!!""№;!";%!"%№!%
                }));
            }
            _neededToAdding.Clear();
        }



        private string AskedAboutRenamed(files files)
        {
            return "";
        }


        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            listUsers0.AllowDrop = false;
            if (pathbox0.Text != "" && pathbox1.Text != "")
                listUsers1.AllowDrop = true;
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(listUsers0, listUsers0.SelectedItems, DragDropEffects.Copy);
                //AddingInDonwloadList(listUsers0.SelectedItems,);
            }
        }

        private void ListViewItem_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (pathbox0.Text != ""&&pathbox1.Text!="")
                listUsers0.AllowDrop = true;
            listUsers1.AllowDrop = false;
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(listUsers1, listUsers1.SelectedItems, DragDropEffects.Copy);
            }
        }

        #region dependency 
        public bool IsCheckBoxChecked
        {
            get { return (bool)GetValue(IsCheckBoxCheckedProperty); }
            set { SetValue(IsCheckBoxCheckedProperty, value); }
        }
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckBoxCheckedProperty =
            DependencyProperty.Register("IsCheckBoxChecked", typeof(bool),
            typeof(MainWindow), new UIPropertyMetadata(false));

        private void transmit_chk_Click(object sender, RoutedEventArgs e)
        {
            if (IsCheckBoxChecked == false)
            {
                IsCheckBoxChecked = true;

            }
            else
            {
                IsCheckBoxChecked = false;

            }
        }

        #endregion

        // object files, string from, string to, System.Drawing.Image image, string ip_from, string ip_to
        private void AddingInDonwloadList(object obj)
        {

            dynamic MyObj = obj;

            object files=MyObj.Item1; 
            string from= MyObj.Item2; 
            string to= MyObj.Item3; 
            System.Drawing.Image image= MyObj.Item4; 
            string ip_from= MyObj.Item5; 
            string ip_to= MyObj.Item6;

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            ()=>
            {
                files file = (files)files;

                if (ip_from == "Этот компьютер")
                {
                    if (file.sizeFile == "")
                    {
                        WpfControlLibrary3.UserControl1 papka = new WpfControlLibrary3.UserControl1();

                        papka.Ip_From = ip_from;
                        papka.Ip_To = ip_to;
                        papka.Path_From = from;
                        papka.Path_To = to + file.nameFile;


                        Thread receiveThread = new Thread(new ParameterizedThreadStart(papka.CheckingDirectory));
                        receiveThread.IsBackground = true;
                        receiveThread.Start(papka.Path_To);

                        tiktak.Items.Add(papka);
                    }
                    else
                    {
                        UserControl1 flk = new UserControl1();
                        flk.Ip_From = ip_from;
                        flk.Ip_To = ip_to;
                        flk.Path_From = from;
                        flk.Path_To = to + file.nameFile;
                        //userControl1.files_inf = (WpfControlLibrary2.files)files;

                        tiktak.Items.Add(flk);

                        //var client = new TcpFileClient(ip_from);

                        //Подписываемся на события
                        //client.SendingEvent += flk.ChangedvalueForProgressBar;
                        //client.FailEvent += flk.SendingFailMessage;
                        //client.ReadyEvent += flk.SendingSuccessfullyMessage;


                        //var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe,flk.Path_To+ "|"+flk.Path_From);

                    }

                }
                else
                {
                    if (file.sizeFile == "")
                    {
                        WpfControlLibrary3.UserControl1 papka = new WpfControlLibrary3.UserControl1();

                        papka.Ip_From = ip_from;
                        papka.Ip_To = ip_to;
                        papka.Path_From = from;
                        papka.Path_To = to + file.nameFile;


                        Thread receiveThread = new Thread(new ParameterizedThreadStart(papka.CheckingDirectory));
                        receiveThread.IsBackground = true;
                        receiveThread.Start(papka.Path_To);

                        tiktak.Items.Add(papka);
                    }
                    else
                    {
                        UserControl1 flk = new UserControl1();
                        flk.Ip_From = ip_from;
                        flk.Ip_To = ip_to;
                        flk.Path_From = from;
                        flk.Path_To = to + file.nameFile;
                        //userControl1.files_inf = (WpfControlLibrary2.files)files;

                        tiktak.Items.Add(flk);

                        //var client = new TcpFileClient(ip_from);

                        ////Подписываемся на события
                        //client.SendingEvent += flk.ChangedvalueForProgressBar;
                        //client.FailEvent += flk.SendingFailMessage;
                        //client.ReadyEvent += flk.SendingSuccessfullyMessage;

                        //string path = flk.Path_To + "|" + flk.Path_From;
                        //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                        //receiveThread.IsBackground = true;
                        //receiveThread.Start(path);
                    }
                }

                
            }));
        

        }

        private void Btn_moreInfo_Click(object sender, RoutedEventArgs e)
        {
            dynamic d = sender;
            //object predGrid
        }

        private void ClearList_btn_Click(object sender, RoutedEventArgs e)
        {
            //AddingInDonwloadList(null, null, null, null,null,null);


            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
             () =>
             {

                WpfControlLibrary3.UserControl1 userControl1 = new WpfControlLibrary3.UserControl1();                
                //userControl1.files_inf = (WpfControlLibrary2.files)files;
                tiktak.Items.Add(userControl1);

            }));

        }
    }
}
