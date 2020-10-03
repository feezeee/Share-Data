using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public class TcpFileClient
    {
        private static int port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);

        public delegate void SendingMethod(double procents);
        public event SendingMethod SendingEvent;

        public delegate void Failmethod();
        public event Failmethod FailEvent;

        public delegate void ReadyMethod();
        public event ReadyMethod ReadyEvent;

        public IPAddress RemoteIP;
        private TcpClient client = new TcpClient();
        public TcpFileClient(string ip)
        {
            RemoteIP = IPAddress.Parse(ip);
        }

        public void SendRequest(RequestTipe request, string messege)
        {
        }

        public async void SendFileRequestFromMe(object obj)
        {
            await Task.Run(() =>
            {
                try
                {
                    string[] mes = obj.ToString().Split('|');
                    string localPathFile = mes[1];
                    string remotePathToSave = mes[0];
                    var reciverEP = new IPEndPoint(RemoteIP, port);
                    client.Connect(reciverEP);// Подключаемся к удаленному пк
                    while (!client.Connected) ;
                    var connectedStream = client.GetStream();

                    //Request.DoSendingRequest()
                    var mess = $"Request|{(int)RequestTipe.SendFileFromMe}|{remotePathToSave}";
                    var buf = Encoding.UTF8.GetBytes(mess);
                    connectedStream.Write(buf, 0, buf.Length);//Отсылаем сообщение куда будем сохранять файл
                    Int64 bytesReceived = 0;
                    byte[] buffer = new byte[1024 * 8];
                    //connectedStream.Read(buffer, 0, 1024);//Ожидаем ответа

                    byte[] mes_result1 = new byte[1024 * 8];
                    var requestLen1 = connectedStream.Read(mes_result1, 0, 1024);//Ждем ответ
                    string request1 = Encoding.UTF8.GetString(mes_result1, 0, requestLen1);


                    if (request1 == "ready")//Если он готов получить файл
                    {
                        

                        if (!string.IsNullOrEmpty(localPathFile))
                        { // файл есть, отдаём
                            using (var fileIO = File.OpenRead(localPathFile))
                            {
                                //stream.Write(BitConverter.GetBytes(fileIO.Length), 0, 8);
                                var b = BitConverter.GetBytes(fileIO.Length);
                                //fileIO.Length.ToString().Length
                                //stream.Write(b, 0, fileIO.Length.ToString().Length);
                                connectedStream.Write(b, 0, b.Length);//Отсылаем размер файла

                                var buffer1 = new byte[1024 * 8];
                                int count;

                                double allSize = fileIO.Length;
                                double howTransmit = 0;
                                while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    connectedStream.Write(buffer, 0, count);
                                    howTransmit += count;
                                    double res = howTransmit / allSize * 100;
                                    SendingEvent?.Invoke(res);
                                }
                            }
                        }
                        else
                        {// файл отсутствует 
                            var data = BitConverter.GetBytes((int)RequestError.FileNotExist);
                            connectedStream.Write(data, 0, data.Length);
                        }
                    }
                    else
                    {
                        FailEvent?.Invoke();
                    }

                    byte[] mes_result = new byte[1024 * 8];
                    var requestLen = connectedStream.Read(mes_result, 0, 1024);//Ждем ответ
                    string request = Encoding.UTF8.GetString(mes_result, 0, requestLen);
                    if(request== "successfully")
                    {
                        ReadyEvent?.Invoke();
                    }   
                    else
                    {
                        FailEvent?.Invoke();
                    }

                    connectedStream.Close();
                    client.Close();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    FailEvent?.Invoke();
                }
            });
        }

        /// <summary>
        /// куда|откуда
        /// </summary>
        /// <param name="obj"></param>
        public async void SendFileRequest(object obj)
        {
            await Task.Run(() =>
            {
                try
                {
                    string[] mes = obj.ToString().Split('|');
                    string remoteFilePath = mes[1];
                    string localPathToSave = mes[0];
                    var reciverEP = new IPEndPoint(RemoteIP, port);
                    client.Connect(reciverEP);
                    while (!client.Connected) ;
                    var connectedStream = client.GetStream();

                    //Request.DoSendingRequest()
                    var mess = $"Request|{(int)RequestTipe.SendFile}|{remoteFilePath}";
                    var buf = Encoding.UTF8.GetBytes(mess);
                    connectedStream.Write(buf, 0, buf.Length);
                    Int64 bytesReceived = 0;
                    int count;
                    byte[] buffer = new byte[1024 * 8];
                    connectedStream.Read(buffer, 0, 1024);


                    Int64 fileBytesSize = BitConverter.ToInt64(buffer, 0);

                    if (fileBytesSize == -1)
                    {

                    } // файл был не найден
                    if (fileBytesSize == -2)
                    {

                    }// запрос небыл обработан
                    bool peremen = Execute_(localPathToSave);
                    if (peremen)
                    {
                        using (var fileIO = File.Create(localPathToSave))
                        {
                            while (bytesReceived < fileBytesSize && (count = connectedStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fileIO.Write(buffer, 0, count);

                                bytesReceived += count;
                                double first = bytesReceived; double second = fileBytesSize;
                                SendingEvent?.Invoke(first / second * 100);
                            }
                            SendingEvent?.Invoke(100);
                        }

                        ReadyEvent?.Invoke();
                    }
                    else
                    {
                        FailEvent?.Invoke();
                    }
                    connectedStream.Close();
                    client.Close();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    FailEvent?.Invoke();
                }
            });
        }
        private bool Execute_(string path)
        {
            try
            {
                string[] mes = path.Split('\\');
                string ans = mes[0] + '\\';
                //int request = int.Parse(mes[1]);
                for (int i = 1; i < mes.Length - 1; i++)
                {
                    ans += mes[i] + '\\';
                    if (Directory.Exists(ans) == false)
                    {
                        Directory.CreateDirectory(ans);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
