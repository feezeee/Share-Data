using InterfaceV2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WpfControlLibrary2;

namespace ConnectedForm
{
    public class TransmitingFiles
    {
        public delegate void MethodContainer(ClassAboutFilesAdding[] obj);

        //Событие OnCount c типом делегата  OncompleteList.
        public event MethodContainer OnCompleteList;

        MainWindow window = new MainWindow();
        TcpServer TcpServer = new TcpServer();
        public void ChekingAndLoadingFiles(System.Windows.Controls.ListView control_in, System.Windows.Controls.ListView control_from, System.Windows.Controls.TextBox from, System.Windows.Controls.TextBox to, System.Windows.Controls.Label ip_from, System.Windows.Controls.Label ip_to, MainWindow mainWindow, TcpServer tcpServer)
        {
            window = mainWindow;
            TcpServer = tcpServer;
            System.Collections.IList item_from = null;
            System.Collections.IList items_to = null;
            string from_txt = "";
            string to_txt = "";

            string ip_sender = "";
            string ip_receiver = "";

            List<ClassAboutFilesAdding> lst = new List<ClassAboutFilesAdding>();
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
            () =>
            {
                item_from = control_from.SelectedItems;
                items_to = control_in.Items;

                from_txt = from.Text;
                to_txt = to.Text;

                ip_sender = ip_from.Content.ToString();
                ip_receiver = ip_to.Content.ToString();

            }));
            while (true)
            {
                if (from_txt[from_txt.Length - 1] != '\\')
                    from_txt = from_txt.Remove(from_txt.Length - 1);
                else
                    break;
            }
            while (true)
            {
                if (to_txt[to_txt.Length - 1] != '\\')
                    to_txt = to_txt.Remove(to_txt.Length - 1);
                else
                    break;
            }
            try
            {
                foreach (ClassAboutFilesAdding files in item_from)
                {
                    bool status = false;
                    if (files != null)
                    {
                        foreach (ClassAboutFilesAdding files1 in items_to)
                        {
                            if (files1 != null)
                                if (files1.nameFile == files.nameFile)
                                {
                                    status = true;
                                    //_renamedFiles.Add(files);//добавляем в лист для переименования
                                    //AskedAboutRenamed(files);
                                }
                            //files1.nameFile
                        }
                        if (status != true)
                        {
                            if (from_txt != "" && to_txt != "")
                            {

                                ClassAboutFilesAdding adding = new ClassAboutFilesAdding(files.nameFile, files.time, files.sizeFile, from_txt, to_txt, ip_sender, ip_receiver);
                                lst.Add(adding);
                            }
                            //_neededToAdding.Add(member);

                        }
                    }
                }
                ClassAboutFilesAdding[] filesForTransmit = new ClassAboutFilesAdding[lst.Count];
                // Формирование filesForTransmit
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i] != null)
                        filesForTransmit[i] = lst[i];
                }
                // Вызов события по окончании формирование списка
                OnCompleteList?.Invoke(filesForTransmit);
            }
            catch { }


        }

        //Вызывается при событии OnCompleteList
        public async void CalculatingPathForTransmitting(ClassAboutFilesAdding[] filesForTransmit)
        {
            await Task.Run(() => _calculatingPathForTransmitting(filesForTransmit, TcpServer));
        }



        public async void _calculatingPathForTransmitting(object obj, TcpServer tcpServer)
        {
            ClassAboutFilesAdding[] filesForTransmit = (ClassAboutFilesAdding[])obj;
            for (int i = 0; i < filesForTransmit.Length; i++)
            {
                // Если отправитель текущий пк
                if (filesForTransmit[i].Sender == "Этот компьютер")
                {
                    // Если это файл
                    if (filesForTransmit[i].sizeFile != "")
                    {

                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                        () =>
                        {
                            UserControl1 flk = new UserControl1();

                            flk.Name_ = filesForTransmit[i].nameFile;
                            flk.Ip_From = filesForTransmit[i].Sender;
                            flk.Ip_To = filesForTransmit[i].Receiver;
                            flk.Path_From = filesForTransmit[i].RootLocationFilesOrDirectory;
                            flk.Path_To = filesForTransmit[i].RemoteLocationFilesOrDirectory;


                            window.tiktak.Items.Add(new MenuItem() { Title = filesForTransmit[i].nameFile, flk = flk });



                            var client = new TcpFileClient(filesForTransmit[i].Receiver);

                            //Подписываемся на события
                            client.SendingEvent += flk.ChangedvalueForProgressBar;
                            client.FailEvent += flk.SendingFailMessage;
                            client.ReadyEvent += flk.SendingSuccessfullyMessage;

                            ////var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe, flk.Path_To + "|" + flk.Path_From);

                            string path = flk.Path_To + filesForTransmit[i].nameFile + "|" + flk.Path_From + filesForTransmit[i].nameFile;

                            client.SendFileRequestFromMe(path); // Запуск асинхронного метода


                            //var client = new TcpFileClient(filesForTransmit[i].Receiver);

                            //Подписываемся на события
                            //client.SendingEvent += flk.ChangedvalueForProgressBar;
                            //client.FailEvent += flk.SendingFailMessage;
                            //client.ReadyEvent += flk.SendingSuccessfullyMessage;

                            ////var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe, flk.Path_To + "|" + flk.Path_From);

                            //string path = flk.Path_To + filesForTransmit[i].nameFile + "|" + flk.Path_From + filesForTransmit[i].nameFile;
                            //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                            //receiveThread.IsBackground = true;
                            //receiveThread.Start(path);
                        }));
                        // var ans = RequestInteractivity.SendRequst(filesForTransmit[i].Receiver, RequestTipe.GetFileFromMe, filesForTransmit[i].RemoteLocationFilesOrDirectory + filesForTransmit[i].nameFile + "|" + filesForTransmit[i].RootLocationFilesOrDirectory + filesForTransmit[i].nameFile);
                    }
                    //  Если это папка
                    else
                    {
                        await Task.Run(() => _calculatingForDirectory(filesForTransmit[i]));
                    }

                }
                // Если отправитель удаленный пк
                else
                {
                    // Если это файл
                    if (filesForTransmit[i].sizeFile != "")
                    {

                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                        () =>
                        {
                            UserControl1 flk = new UserControl1();

                            flk.Name_ = filesForTransmit[i].nameFile;
                            flk.Ip_From = filesForTransmit[i].Sender;
                            flk.Ip_To = filesForTransmit[i].Receiver;
                            flk.Path_From = filesForTransmit[i].RootLocationFilesOrDirectory;
                            flk.Path_To = filesForTransmit[i].RemoteLocationFilesOrDirectory;


                            window.tiktak.Items.Add(new MenuItem() { Title = filesForTransmit[i].nameFile, flk = flk });


                            var client = new TcpFileClient(filesForTransmit[i].Sender);

                            //Подписываемся на события
                            client.SendingEvent += flk.ChangedvalueForProgressBar;
                            client.FailEvent += flk.SendingFailMessage;
                            client.ReadyEvent += flk.SendingSuccessfullyMessage;

                            ////var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe, flk.Path_To + "|" + flk.Path_From);

                            string path = flk.Path_To + filesForTransmit[i].nameFile + "|" + flk.Path_From + filesForTransmit[i].nameFile;

                            client.SendFileRequest(path); // Запуск асинхронного метода
                        }));

                    }
                    // Елси это папка
                    else
                    {
                        await Task.Run(() => _calculatingForDirectory(filesForTransmit[i]));
                    }


                }
            }
        }

        object sbros = null;

        private MenuItem CreateDirectoryNodeForRemote(object direct)
        {
            ClassAboutFilesAdding directoryInfo = (ClassAboutFilesAdding)direct;

            string ip_sender = "";
            if (directoryInfo.Sender == "Этот компьютер")
            {
                ip_sender = "127.0.0.1";
            }
            else
            {
                ip_sender = directoryInfo.Sender;
            }

            var ans = RequestInteractivity.SendRequst(ip_sender, RequestTipe.GetDirectoryFiles, directoryInfo.RootLocationFilesOrDirectory + directoryInfo.nameFile); //Получить папки от удаленного пк (здесь от отправителя)


            if (ans[0].ToString() == "A")
            {
                ans = ans.Remove(0, 7);


                var files = ans.Split('\n');
                files[files.Length - 1] = null;

                object papka_ = null;
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                () =>
                {
                    WpfControlLibrary3.UserControl1 papka = new WpfControlLibrary3.UserControl1();

                    papka.Ip_From = directoryInfo.Sender;
                    papka.Ip_To = directoryInfo.Receiver;
                    papka.Path_From = directoryInfo.RootLocationFilesOrDirectory + directoryInfo.nameFile;
                    papka.Path_To = directoryInfo.RemoteLocationFilesOrDirectory + directoryInfo.nameFile;
                    papka.IsHeightValue = files.Length - 1;
                    if (sbros != null)
                    {
                        WpfControlLibrary3.UserControl1 tyc = (WpfControlLibrary3.UserControl1)sbros;
                        papka.OnCompleteTransmit += tyc.ChangedvalueForProgressBar;
                    }
                    papka_ = papka;

                }));

                var directoryNode = new MenuItem() { Title = directoryInfo.nameFile, flk = papka_ };//создаем фалй или папку, пока что мы не знаем


                foreach (var file in files)
                {
                    if (file != null)
                    {
                        string name = "";

                        List<(string, string, string)> ps = CuttingMessages(file);

                        if (ps[0].Item3 == "-1")
                            name = ps[0].Item1 + "\\";
                        else
                            name = ps[0].Item1;

                        ClassAboutFilesAdding files1 = new ClassAboutFilesAdding() // создаём экземпляр класса        
                        {
                            nameFile = ps[0].Item1, // указываем имя файла  
                            time = ps[0].Item2, // указываем время создания    
                            sizeFile = ps[0].Item3, // указываем размер  
                            LocalParentPath = directoryInfo.RootLocationFilesOrDirectory,
                            LocalParentName = directoryInfo.nameFile,
                            RootLocationFilesOrDirectory = directoryInfo.RootLocationFilesOrDirectory + directoryInfo.nameFile + "\\",
                            Receiver = directoryInfo.Receiver,
                            Sender = directoryInfo.Sender,
                            RemoteLocationFilesOrDirectory = directoryInfo.RemoteLocationFilesOrDirectory + directoryInfo.nameFile + "\\",
                            RemoteParentName = directoryInfo.nameFile,
                            RemoteParentPath = directoryInfo.RemoteLocationFilesOrDirectory

                        };

                        if (ps[0].Item3 == "" || ps[0].Item3 == "-1")
                        {
                            sbros = directoryNode.flk;
                            directoryNode.Items.Add(CreateDirectoryNodeForRemote(files1));
                        }
                        else
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                               () =>
                               {
                                   UserControl1 flk = new UserControl1();
                                   flk.Ip_From = files1.Sender;
                                   flk.Ip_To = files1.Receiver;
                                   flk.Path_From = files1.RootLocationFilesOrDirectory;
                                   flk.Path_To = files1.RemoteLocationFilesOrDirectory;
                                   flk.NameFile = files1.nameFile;
                                   WpfControlLibrary3.UserControl1 _papka = (WpfControlLibrary3.UserControl1)directoryNode.flk;
                                   flk.OnCompleteTransmit += _papka.ChangedvalueForProgressBar;

                                   if (files1.Sender != "Этот компьютер")
                                   {

                                       var client = new TcpFileClient(files1.Sender);

                                       //Подписываемся на события
                                       client.SendingEvent += flk.ChangedvalueForProgressBar;
                                       client.FailEvent += flk.SendingFailMessage;
                                       client.ReadyEvent += flk.SendingSuccessfullyMessage;

                                       ////var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe, flk.Path_To + "|" + flk.Path_From);

                                       string path = flk.Path_To + files1.nameFile + "|" + flk.Path_From + files1.nameFile;
                                       client.SendFileRequest(path); //Запускаем процесс получения файла
                                   }
                                   else
                                   {
                                       var client = new TcpFileClient(files1.Receiver);

                                       //Подписываемся на события
                                       client.SendingEvent += flk.ChangedvalueForProgressBar;
                                       client.FailEvent += flk.SendingFailMessage;
                                       client.ReadyEvent += flk.SendingSuccessfullyMessage;

                                       ////var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe, flk.Path_To + "|" + flk.Path_From);

                                       string path = flk.Path_To + files1.nameFile + "|" + flk.Path_From + files1.nameFile;
                                       client.SendFileRequest(path); //Запускаем процесс получения файла
                                   }

                                   directoryNode.Items.Add(new MenuItem() { Title = ps[0].Item1, flk = flk });

                               }));
                            
                        }
                    }
                }

                return directoryNode;
            }
            else
                return null;

        }

        private MenuItem CreateDirectoryNodeForLocal(object direct)
        {
            ClassAboutFilesAdding directoryInfo = (ClassAboutFilesAdding)direct;

            string ip_sender = "";
            if (directoryInfo.Sender == "Этот компьютер")
            {
                ip_sender = "127.0.0.1";
            }
            else
            {
                ip_sender = directoryInfo.Sender;
            }


            var ans = GetDirectoryLocal(directoryInfo.RootLocationFilesOrDirectory + directoryInfo.nameFile);

            if (ans != "False")
            {
                var files = ans.Split('\n');
                files[files.Length - 1] = null;

                object papka_ = null;
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                () =>
                {
                    WpfControlLibrary3.UserControl1 papka = new WpfControlLibrary3.UserControl1();

                    papka.Ip_From = directoryInfo.Sender;
                    papka.Ip_To = directoryInfo.Receiver;
                    papka.Path_From = directoryInfo.RootLocationFilesOrDirectory + directoryInfo.nameFile;
                    papka.Path_To = directoryInfo.RemoteLocationFilesOrDirectory + directoryInfo.nameFile;
                    papka.IsHeightValue = files.Length - 1;
                    if (sbros != null)
                    {
                        WpfControlLibrary3.UserControl1 tyc = (WpfControlLibrary3.UserControl1)sbros;
                        papka.OnCompleteTransmit += tyc.ChangedvalueForProgressBar;
                    }
                    papka_ = papka;

                }));

                var directoryNode = new MenuItem() { Title = directoryInfo.nameFile, flk = papka_ };//создаем фалй или папку, пока что мы не знаем


                foreach (var file in files)
                {
                    if (file != null)
                    {
                        string name = "";

                        List<(string, string, string)> ps = CuttingMessages(file);

                        if (ps[0].Item3 == "-1")
                            name = ps[0].Item1 + "\\";
                        else
                            name = ps[0].Item1;

                        ClassAboutFilesAdding files1 = new ClassAboutFilesAdding() // создаём экземпляр класса        
                        {
                            nameFile = ps[0].Item1, // указываем имя файла  
                            time = ps[0].Item2, // указываем время создания    
                            sizeFile = ps[0].Item3, // указываем размер  
                            LocalParentPath = directoryInfo.RootLocationFilesOrDirectory,
                            LocalParentName = directoryInfo.nameFile,
                            RootLocationFilesOrDirectory = directoryInfo.RootLocationFilesOrDirectory + directoryInfo.nameFile + "\\",
                            Receiver = directoryInfo.Receiver,
                            Sender = directoryInfo.Sender,
                            RemoteLocationFilesOrDirectory = directoryInfo.RemoteLocationFilesOrDirectory + directoryInfo.nameFile + "\\",
                            RemoteParentName = directoryInfo.nameFile,
                            RemoteParentPath = directoryInfo.RemoteLocationFilesOrDirectory

                        };

                        if (ps[0].Item3 == "" || ps[0].Item3 == "-1")
                        {
                            sbros = directoryNode.flk;
                            directoryNode.Items.Add(CreateDirectoryNodeForLocal(files1));
                        }
                        else
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                               () =>
                               {
                                   UserControl1 flk = new UserControl1();
                                   flk.Ip_From = files1.Sender;
                                   flk.Ip_To = files1.Receiver;
                                   flk.Path_From = files1.RootLocationFilesOrDirectory;
                                   flk.Path_To = files1.RemoteLocationFilesOrDirectory;
                                   WpfControlLibrary3.UserControl1 _papka = (WpfControlLibrary3.UserControl1)directoryNode.flk;
                                   flk.OnCompleteTransmit += _papka.ChangedvalueForProgressBar;

                                   if (files1.Sender != "Этот компьютер")// Если отправитель удаленный пк
                                   {

                                       var client = new TcpFileClient(files1.Sender);

                                       //Подписываемся на события
                                       client.SendingEvent += flk.ChangedvalueForProgressBar;
                                       client.FailEvent += flk.SendingFailMessage;
                                       client.ReadyEvent += flk.SendingSuccessfullyMessage;


                                       string path = flk.Path_To+ files1.nameFile + "|" + flk.Path_From + files1.nameFile;
                                       client.SendFileRequest(path);

                                       //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                                       //receiveThread.IsBackground = true;
                                       //receiveThread.Start(path);
                                   }
                                   else// если отправитель текущий пк
                                   {
                                       var client = new TcpFileClient(files1.Receiver);

                                       //Подписываемся на события
                                       client.SendingEvent += flk.ChangedvalueForProgressBar;
                                       client.FailEvent += flk.SendingFailMessage;
                                       client.ReadyEvent += flk.SendingSuccessfullyMessage;


                                       string path = flk.Path_To + files1.nameFile + "|" + flk.Path_From + files1.nameFile;
                                       client.SendFileRequest(path);
                                   }

                                   directoryNode.Items.Add(new MenuItem() { Title = ps[0].Item1, flk = flk });

                               }));

                        }
                    }
                }

                return directoryNode;
            }
            else
                return null;

        }

        private string GetDirectoryLocal(string directoryPass)
        {
            try
            {
                string ans = "";

                var direct = new DirectoryInfo(directoryPass);
                foreach (var dir in direct.GetDirectories())
                {

                    ans += $"{dir.Name}|{dir.CreationTime.ToString()}|{-1}\n";
                }
                foreach (var file in direct.GetFiles())
                {
                    ans += $"{file.Name}|{file.CreationTime.ToString()}|{file.Length}\n";
                }

                return ans;
            }
            catch
            {
                return "Fail";
            }
        }

        public void _calculatingForDirectory(object rootDirectory)
        {
            ClassAboutFilesAdding directoryInfo = (ClassAboutFilesAdding)rootDirectory;

            MenuItem treeNode = new MenuItem();

            if (directoryInfo.Sender != "Этот компьютер" && directoryInfo.Sender != "127.0.0.1")
            {
                treeNode = CreateDirectoryNodeForRemote(rootDirectory);
            }
            else
            {
                treeNode = CreateDirectoryNodeForLocal(rootDirectory);// Если с локального куда-то
            }


            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
            () =>
            {
                window.tiktak.Items.Add(treeNode);

            }));
            //int countDirectory = 0;

            //List<ClassAboutFilesAdding> lst = new List<ClassAboutFilesAdding>();


            //ClassAboutFilesAdding directoryForTransmit = (ClassAboutFilesAdding)rootDirectory;
            //directoryForTransmit.RootLocationFilesOrDirectory += directoryForTransmit.nameFile + "\\";

            //directoryForTransmit.RemoteLocationFilesOrDirectory += "\\";

            //lst.Add(directoryForTransmit);

            //string ip_sender;

            //string path = directoryForTransmit.RootLocationFilesOrDirectory;

            //if (directoryForTransmit.Sender == "Этот компьютер")
            //{
            //    ip_sender = "127.0.0.1";
            //}
            //else
            //{
            //    ip_sender = directoryForTransmit.Sender;
            //}



            //for (int i = 0; i < lst.Count; i++)
            //{
            //    if (lst[i].sizeFile == "-1" || lst[i].sizeFile == "")
            //    {
            //        countDirectory++;
            //        var ans = RequestInteractivity.SendRequst(ip_sender, RequestTipe.GetDirectoryFiles, lst[i].RootLocationFilesOrDirectory); //Получить папки от удаленного пк (здесь от отправителя)

            //        if (ans[0].ToString() == "A")
            //        {
            //            ans = ans.Remove(0, 7);
            //        }

            //        var files = ans.Split('\n');
            //        files[files.Length - 1] = null;

            //        //TreeView treeView = new TreeView();


            //        foreach (var file in files)
            //        {
            //            if (file != null)
            //            {
            //                List<(string, string, string)> ps = CuttingMessages(file);
            //                string name = "";
            //                if (ps[0].Item3 == "-1")
            //                    name = ps[0].Item1 + "\\";
            //                else
            //                    name = ps[0].Item1;
            //                ClassAboutFilesAdding files1 = new ClassAboutFilesAdding() // создаём экземпляр класса        
            //                {
            //                    nameFile = ps[0].Item1, // указываем имя файла  
            //                    time = ps[0].Item2, // указываем время создания    
            //                    sizeFile = ps[0].Item3, // указываем размер  
            //                    LocalParentPath = lst[i].RootLocationFilesOrDirectory,
            //                    LocalParentName = lst[i].nameFile,
            //                    RootLocationFilesOrDirectory = lst[i].RootLocationFilesOrDirectory + name,
            //                    Receiver = lst[i].Receiver,
            //                    Sender = lst[i].Sender,
            //                    RemoteLocationFilesOrDirectory = lst[i].RemoteLocationFilesOrDirectory + name,
            //                    RemoteParentName = lst[i].nameFile,
            //                    RemoteParentPath = lst[i].RemoteLocationFilesOrDirectory

            //                };

            //                lst.Add(files1);
            //            }
            //        }
            //    }
            //}




            //int cout = 1;



            //List<MenuItem> lst_menu = new List<MenuItem>();
            //MenuItem root = new MenuItem();

            //root.Title = lst[0].nameFile;

            //System.Windows.Forms.TreeView treeView = new System.Windows.Forms.TreeView();
            //TreeNode treeNode = new TreeNode();


            //MenuItem child = new MenuItem();

            ////string nameParent = lst[lst.Count - 1].LocalParentName;

            //for (int i = 0; i < lst.Count; i++)
            //{
            //    Console.WriteLine(lst[i].nameFile);
            //}

            //string nameparent = lst[0].nameFile;



            //for (int i = lst.Count-1; i >= 0; i--)
            //{
            //    if (lst[i].LocalParentName != nameParent)
            //    {                      
            //        lst_menu.Add(child);
            //        //child.Title = lst[i].nameFile;                    
            //        nameParent = lst[i].LocalParentName;
            //        child = new MenuItem();
            //    }

            //    if (lst[i].LocalParentName == nameParent && (lst[i].sizeFile != "-1" && lst[i].sizeFile != ""))
            //    {
            //        child.Items.Add(new MenuItem() { Title = lst[i].nameFile, NameParent = lst[i].LocalParentName });
            //        child.Title = lst[i].LocalParentName;
            //    }
            //    else if(lst[i].sizeFile=="-1"|| lst[i].sizeFile=="")
            //    {
            //        //lst_menu.Add(child);
            //        //child = new MenuItem();

            //        string namecheck = lst[i].nameFile;
            //        bool status = false;
            //        MenuItem child2 = new MenuItem();
            //        for (int k = cout; k < lst_menu.Count; k++)
            //        {
            //            if(lst_menu[k].Title==namecheck||lst_menu[k].NameParent==namecheck)
            //            {
            //                lst_menu[cout].NameParent = lst[i].LocalParentName;                           
            //                child2.Items.Add(lst_menu[cout]);

            //                //child2.Items.Add(lst_menu[cout]);

            //                child2.Title = lst[i].LocalParentName;

            //                //child2.Title = lst[i].LocalParentName;

            //                cout++;
            //            }

            //            //if (lst_menu[k].Title == namecheck)
            //            //{
            //            //    //lst_menu[cout].NameParent = lst[i].LocalParentName;
            //            //    //child2.Items.Add(lst_menu[cout]);
            //            //    ////child2.Items.Add(lst_menu[cout]);
            //            //    //child2.Title = lst[i].LocalParentName;
            //            //    ////child2.Title = lst[i].LocalParentName;
            //            //    //cout++;
            //            //}
            //            //else
            //            //{
            //            //    //nameParent = lst[i].LocalParentName;
            //            //    //child2.Title = lst[i].nameFile;

            //            //    break;
            //            //}

            //        }
            //        lst_menu.Add(child2);
            //    }


            //if(lst[i].LocalParentName!=nameParent)
            //{
            //    lst_menu.Add(child);
            //    //child.Title = lst[i].nameFile;                    
            //    nameParent = lst[i].LocalParentName;                    
            //    child= new MenuItem();
            //}
            //if (lst[i].LocalParentName == nameParent && (lst[i].sizeFile != "-1" && lst[i].sizeFile != "")) 
            //{
            //    child.Items.Add(new MenuItem() { Title = lst[i].nameFile, NameParent=lst[i].LocalParentName }) ;
            //    child.Title = lst[i].LocalParentName;
            //}
            //else if(lst[i].sizeFile=="-1"|| lst[i].sizeFile=="")
            //{
            //    lst_menu.Add(child);
            //    child = new MenuItem();
            //    string namecheck = lst[i].nameFile;
            //    MenuItem child2 = new MenuItem();
            //    for (int k = cout; k < lst_menu.Count; k++)
            //    {   
            //        if(lst_menu[k].Title==namecheck)
            //        {
            //            lst_menu[cout].NameParent= lst[i].LocalParentName;
            //            child2.Items.Add(lst_menu[cout]);
            //            //child2.Items.Add(lst_menu[cout]);
            //            child2.Title = lst[i].LocalParentName;
            //            //child2.Title = lst[i].LocalParentName;
            //            cout++;
            //        }
            //        else 
            //        {
            //            nameParent = lst[i].LocalParentName;
            //            //child2.Title = lst[i].nameFile;
            //            lst_menu.Add(child2);                            
            //            break;
            //        }

            //    }
            //}

            ////}
            //MenuItem menuItem = new MenuItem();
            //menuItem = lst_menu[lst_menu.Count - 1].Items[0];
            ////while (true)
            ////{

            ////}
            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
            //    () =>
            //    {
            //        //TreeView treeView = new TreeView();
            //        //treeView.Items.Add(lst_menu[lst_menu.Count - 1]);
            //        window.tiktak.Items.Add(menuItem);
            //    }));
            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
            //() =>
            //{

            //    TreeView treeView = new TreeView();

            //    TreeView treeView1 = new TreeView();

            //    TreeView treeView2 = new TreeView();
            //    string name = lst[0].nameFile;

            //    for(int i = 0; i<lst.Count;i++)
            //    {
            //        //if(lst[i].LocalParentName==null)
            //        //{
            //        //    treeView1.Items.Add(lst[i].nameFile);                        
            //        //}
            //        //else if(lst[i].LocalParentName == name && lst[i].sizeFile=="-1")
            //        //{

            //        //}
            //        //treeView.Items.Add(lst[i].nameFile);
            //        window.tiktak.Items.Add(lst[i].nameFile);
            //    }

            //}));
        }





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
        public class MenuItem
        {

            public MenuItem()
            {
                this.Items = new ObservableCollection<MenuItem>();
            }

            public string Title { get; set; }
            public string NameParent { get; set; }
            public object flk { get; set; }
            public ObservableCollection<MenuItem> Items { get; set; }

        }
    }

}
