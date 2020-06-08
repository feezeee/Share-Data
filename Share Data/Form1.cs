using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interface;

namespace Share_Data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Drawing(getting_name.get_name(),10,10,this);
        }

        public bool Status = false;

        private void rounded_Button1_Click(object sender, EventArgs e)
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
           
        }
        int x = 500;
        int y = 100;
        int count = 0;

        public void Draw(object sender,List<string> lst)
        {            
            //В переменной lst содержится лист с ip адресами
            Drawing(lst[count], x, y, this);
            //Здесь производим расчет координат
            count++;
            y += 20;
            
        }
              

        public void Drawing(string text, int x, int y, Form form)
        {
            if (this.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    Label label = new Label();
                    label.Text = text;
                    label.AutoSize = true;
                    label.Location = new Point(x, y);
                    label.MaximumSize = new Size(400, 0);
                    form.Controls.Add(label);
                });

                //после повторного вызова этого метода через Invoke, infoLabel.InvokeRequired
                //уже будет "думать", что обращаемся к контролу из основного потока (в котором был создан контрол).
                //Если вызвать этот метод из потока, в котором был создан конрол, то вызовется сразу присвоение текста
            }
            else
            {
                try
                {
                    Label label = new Label();
                    label.Text = text;
                    label.AutoSize = true;
                    label.Location = new Point(x, y);
                    label.MaximumSize = new Size(400, 0);
                    form.Controls.Add(label);
                }
                catch { }
            }
        }//Добавляем label в указанную область 
    }
}
