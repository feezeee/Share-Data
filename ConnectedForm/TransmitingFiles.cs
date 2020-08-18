using InterfaceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public void ChekingAndLoadingFiles(ListView control_in, ListView control_from, TextBox from, TextBox to, Label ip_from, Label ip_to, MainWindow mainWindow)
        {
            window = mainWindow;
            System.Collections.IList item_from = null;
            System.Collections.IList items_to = null;
            string from_txt = "";
            string to_txt = "";
            
            string ip_sender = "";
            string ip_receiver = "";

            List<ClassAboutFilesAdding> lst = new List<ClassAboutFilesAdding>();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
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

                                ClassAboutFilesAdding adding = new ClassAboutFilesAdding(files.nameFile, files.time, files.sizeFile, from_txt, to_txt + files.nameFile, ip_sender, ip_receiver);
                                lst.Add(adding);
                            }
                            //_neededToAdding.Add(member);

                        }
                    }
                }
                ClassAboutFilesAdding[] filesForTransmit = new ClassAboutFilesAdding[lst.Count];
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i] != null)
                        filesForTransmit[i] = lst[i];
                }
                OnCompleteList?.Invoke(filesForTransmit);                
            }
            catch { }


        }

        public void CalculatingPathForTransmitting(ClassAboutFilesAdding[] filesForTransmit)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(_calculatingPathForTransmitting));
            thread.IsBackground = true;
            thread.Start(filesForTransmit);
        }



        public void _calculatingPathForTransmitting(object obj)
        {
            ClassAboutFilesAdding[] filesForTransmit = (ClassAboutFilesAdding[])obj;
            for (int i = 0; i < filesForTransmit.Length; i++)
            {
                if (filesForTransmit[i].Sender == "Этот компьютер")
                {
                    if (filesForTransmit[i].sizeFile != "")
                    {
                        
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                        () =>
                        {
                            UserControl1 flk = new UserControl1();                          

                            
                            flk.Ip_From = filesForTransmit[i].Sender;
                            flk.Ip_To = filesForTransmit[i].Receiver;
                            flk.Path_From = filesForTransmit[i].RootLocationFilesOrDirectory+ filesForTransmit[i].nameFile;
                            flk.Path_To = filesForTransmit[i].RemoteLocationFilesOrDirectory;


                            window.tiktak.Items.Add(flk);


                            var client = new TcpFileClient(filesForTransmit[i].Receiver);

                            //Подписываемся на события
                            client.SendingEvent += flk.ChangedvalueForProgressBar;
                            client.FailEvent += flk.SendingFailMessage;
                            client.ReadyEvent += flk.SendingSuccessfullyMessage;

                            //var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe,flk.Path_To+ "|"+flk.Path_From);
                        
                            string path = flk.Path_To + "|" + flk.Path_From;
                            Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                            receiveThread.IsBackground = true;
                            receiveThread.Start(path);
                        }));
                    }

                    //if (file.sizeFile == "")
                    //{
                    //    Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                    //    () =>
                    //    {
                    //        WpfControlLibrary3.UserControl1 papka = new WpfControlLibrary3.UserControl1();

                    //        papka.Ip_From = ip_from;
                    //        papka.Ip_To = ip_to;
                    //        papka.Path_From = from;
                    //        papka.Path_To = to + file.nameFile;


                    //        Thread receiveThread = new Thread(new ParameterizedThreadStart(papka.CheckingDirectory));
                    //        receiveThread.IsBackground = true;
                    //        receiveThread.Start(papka.Path_To);

                    //        tiktak.Items.Add(papka);
                    //    }));
                    //}
                    //else
                    //{
                    //    Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                    //    () =>
                    //    {
                    //        UserControl1 flk = new UserControl1();
                    //        flk.Ip_From = ip_from;
                    //        flk.Ip_To = ip_to;
                    //        flk.Path_From = from;
                    //        flk.Path_To = to + file.nameFile;
                    //        //userControl1.files_inf = (WpfControlLibrary2.files)files;


                    //        tiktak.Items.Add(flk);


                    //        var client = new TcpFileClient(ip_to);

                    //        //Подписываемся на события
                    //        client.SendingEvent += flk.ChangedvalueForProgressBar;
                    //        client.FailEvent += flk.SendingFailMessage;
                    //        client.ReadyEvent += flk.SendingSuccessfullyMessage;

                    //        //var ans = RequestInteractivity.SendRequst(ip_to, RequestTipe.GetFileFromMe,flk.Path_To+ "|"+flk.Path_From);

                    //        string path = flk.Path_To + "|" + flk.Path_From;
                    //        Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                    //        receiveThread.IsBackground = true;
                    //        receiveThread.Start(path);
                    //    }));

                    //}

                }
                else
                {
                    if (filesForTransmit[i].sizeFile != "")
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                        () =>
                        {
                            UserControl1 flk = new UserControl1();
                            flk.Ip_From = filesForTransmit[i].Sender;
                            flk.Ip_To = filesForTransmit[i].Receiver;
                            flk.Path_From = filesForTransmit[i].RootLocationFilesOrDirectory + filesForTransmit[i].nameFile;
                            flk.Path_To = filesForTransmit[i].RemoteLocationFilesOrDirectory;

                            window.tiktak.Items.Add(flk);
                        }));

                        var ans = RequestInteractivity.SendRequst(filesForTransmit[i].Sender, RequestTipe.GetFileFromMe, filesForTransmit[i].RemoteLocationFilesOrDirectory + "|" + filesForTransmit[i].RootLocationFilesOrDirectory + filesForTransmit[i].nameFile);

                        //var client = new TcpFileClient(ip_from);

                        ////Подписываемся на события
                        //client.SendingEvent += flk.ChangedvalueForProgressBar;
                        //client.FailEvent += flk.SendingFailMessage;
                        //client.ReadyEvent += flk.SendingSuccessfullyMessage;

                        //string path = flk.Path_To + "|" + flk.Path_From;
                        //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                        //receiveThread.IsBackground = true;
                        //receiveThread.Start(path);

                        //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                        //() =>
                        //{
                        //    WpfControlLibrary3.UserControl1 papka = new WpfControlLibrary3.UserControl1();


                        //    papka.Ip_From = ip_from;
                        //    papka.Ip_To = ip_to;
                        //    papka.Path_From = from;
                        //    papka.Path_To = to + file.nameFile;
                        //    tiktak.Items.Add(papka);

                        //    Thread receiveThread = new Thread(new ParameterizedThreadStart(papka.CheckingDirectory));
                        //    receiveThread.IsBackground = true;
                        //    receiveThread.Start(papka.Path_To);
                        //}));

                    }
                    //else
                    //{
                    //    Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(
                    //    () =>
                    //    {
                    //        UserControl1 flk = new UserControl1();
                    //        flk.Ip_From = ip_from;
                    //        flk.Ip_To = ip_to;
                    //        flk.Path_From = from;
                    //        flk.Path_To = to + file.nameFile;
                    //        //userControl1.files_inf = (WpfControlLibrary2.files)files;
                    //        tiktak.Items.Add(flk);
                    //    }));

                    //    var ans = RequestInteractivity.SendRequst(ip_from, RequestTipe.GetFileFromMe, to + file.nameFile + "|" + from);

                    //    //var client = new TcpFileClient(ip_from);

                    //    ////Подписываемся на события
                    //    //client.SendingEvent += flk.ChangedvalueForProgressBar;
                    //    //client.FailEvent += flk.SendingFailMessage;
                    //    //client.ReadyEvent += flk.SendingSuccessfullyMessage;

                    //    //string path = flk.Path_To + "|" + flk.Path_From;
                    //    //Thread receiveThread = new Thread(new ParameterizedThreadStart(client.SendFileRequest));
                    //    //receiveThread.IsBackground = true;
                    //    //receiveThread.Start(path);
                    //}

                }
            }
        }

    }
}
