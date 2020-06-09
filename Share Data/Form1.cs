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
using IPmanip;


namespace Share_Data
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Содержит данные о пк: имя и ip
        /// </summary>
        List<Data_about_PC> _PCs = new List<Data_about_PC>();

        public Form1()
        {
            InitializeComponent();
            Drawing_label("Как вы отображаетесь: "+ SelfName.Name, 10,10,this);
            
            //PictureBox picture = new PictureBox();
            //picture.Image = Properties.Resources.релиз_пк;
            //picture.Width = flowLayoutPanel1.Height-10;
            //picture.Height = flowLayoutPanel1.Height-10;
            //picture.SizeMode = PictureBoxSizeMode.Zoom;

            //flowLayoutPanel1.Controls.Add(picture);
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
            

            Drawing_picture_for_pc(this,"hello","ip");

            //PictureBox picture = new PictureBox();
            //picture.Image = Properties.Resources.релиз_пк;
            //picture.Width = flowLayoutPanel1.Height - 30;
            //picture.Height = flowLayoutPanel1.Height - 30;
            //picture.SizeMode = PictureBoxSizeMode.Zoom;

            //flowLayoutPanel1.Controls.Add(picture);

        }
        int x = 500;
        int y = 100;
        int count = 0;

        public void Draw(object sender,List<(string, string)> lst)
        {
            _PCs.Add(new Data_about_PC { Adress = lst[count].Item2, Name = lst[count].Item1 });

            //В переменной lst содержится лист с ip адресами
            //Drawing_label(lst[count], x, y, this);
            Drawing_picture_for_pc(this, lst[count].Item1, lst[count].Item2);

            //Здесь производим расчет координат
            count++;
            y += 20;            
            
        }

        /// <summary>
        /// Рисует label в координатах с текстом к координатах x,y
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="form"></param>
        public void Drawing_label(string text, int x, int y, Form form)
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
        }
        /// <summary>
        /// Добавляет картинку пк на форму
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="form"></param>
        public void Drawing_picture_for_pc(Form form,string name,string ip)
        {
            if (this.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    Panel panel = new Panel();
                    PictureBox picture = new PictureBox();
                    Label label = new Label();

                    // Initialize the Panel control.                    
                    panel.Size = new Size(flowLayoutPanel1.Height - 30, flowLayoutPanel1.Height - 30);
                    panel.Name = name;
                    // Set the Borderstyle for the Panel to three-dimensional.

                    // Initialize the picturebox control.    
                    picture.Image = Properties.Resources.pc;
                    picture.Width = panel.Height - 30;
                    picture.Height = panel.Height - 30;
                    picture.SizeMode = PictureBoxSizeMode.Zoom;



                    label.Location = new Point(picture.Location.X, picture.Height);
                    label.Text = name;
                    label.Size = new Size(picture.Width, 20);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.AutoSize = false;

                    flowLayoutPanel1.Controls.Add(panel);
                    panel.Controls.Add(picture);
                    panel.Controls.Add(label);
                    ////////////////
                    ///Добавляем обработчик
                    ///
                    panel.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel.MouseLeave += new EventHandler(panel_MouseLeave);
                });

            }
            else
            {
                try
                {
                    Panel panel = new Panel();
                    PictureBox picture = new PictureBox();
                    Label label = new Label();

                    // Initialize the Panel control.                    
                    panel.Size = new Size(flowLayoutPanel1.Height-30, flowLayoutPanel1.Height-30);
                    panel.Name = name;
                    // Set the Borderstyle for the Panel to three-dimensional.

                    // Initialize the picturebox control.    
                    picture.Image = Properties.Resources.pc;
                    picture.Width = panel.Height - 30;
                    picture.Height = panel.Height - 30;
                    picture.SizeMode = PictureBoxSizeMode.Zoom;


                    
                    label.Location = new Point(picture.Location.X, picture.Height);
                    label.Text = name;
                    label.Size = new Size(picture.Width, 20);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.AutoSize = false;

                    flowLayoutPanel1.Controls.Add(panel);
                    panel.Controls.Add(picture);
                    panel.Controls.Add(label);
                    ////////////////
                    ///Добавляем обработчик
                    ///
                    panel.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel.MouseLeave += new EventHandler(panel_MouseLeave);

                }
                catch { }
            }

        }

        private object sendering;
        private string name;
        private void panel_MouseLeave(object sender, EventArgs e)
        {
            flag = false;
            keyTimer = true;
            timer1.Stop();

        }

        private void panel_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            string str = panel.Name;
            name = str;
            string param = flowLayoutPanel1.Controls.Find(name, false).First().Text;


            //flag = true;
            //sendering = sender;
            //Panel panel = (Panel)sendering;
            //firstWidth = panel.Width;
            //timer1.Start();
        }

        int firstWidth;

        public void AddS(int x)
        {
            Panel panel = (Panel)sendering;
            if ((panel.Width <= firstWidth) || (panel.Width >= firstWidth+x*10 ))
                keyTimer = false;
            else keyTimer = true;

            if ((flag) && (keyTimer))
            {
                panel.Width += x;
            }
            else panel.Width -= x;            

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //    if (keyTimer)
            //    {
            //        AddS(2);
            //    }
            //    else timer1.Stop();

        }
        private bool keyTimer = true; 
        private bool flag = false;
    }
}
