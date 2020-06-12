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
using System.Timers;
using System.Windows.Forms;
using Interface;
using IPmanip;
using DrawingAnimations;
using WpfControlLibrary1;

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
            Drawing_label("Как вы отображаетесь: " + SelfName.Name, 10, 10, this);            

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

        public void Draw(object sender, List<(string, string)> lst)
        {
            _PCs.Add(new Data_about_PC { Adress = lst[count].Item2, Name = lst[count].Item1});

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

               

        public int number_for_controls = 2;
        /// <summary>
        /// Добавляет картинку пк на форму
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="form"></param>
        /// 
        public void Drawing_picture_for_pc(Form form, string name, string ip)
        {            
            if (this.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {

                    //count_for_controls++;
                    //Panel panel = new Panel();
                    //Panel panel_up = new Panel();
                    //Panel panel_left = new Panel();
                    //Panel panel_for_picture = new Panel();
                    //Panel dop_panel = new Panel();

                    //PictureBox picture = new PictureBox();
                    //Label label = new Label();
                    //Label label_ip = new Label();
                    //Label label_up = new Label();

                    //// Initialize the Panel control.                    
                    //panel.Size = new Size(flowLayoutPanel1.Height, flowLayoutPanel1.Height-30);
                    //panel.Name = "panel" + count_for_controls.ToString();
                    ////panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //// Set the Borderstyle for the Panel to three-dimensional.

                    /////
                    //panel_up.Name = "panel_up" + count_for_controls.ToString();
                    //panel_up.Size = new Size(panel.Width, panel.Width / 7);
                    //panel_up.MaximumSize = new Size(panel.Width, panel.Width / 7);
                    //panel_up.MinimumSize = new Size(panel.Width, 0);
                    ////panel_up.Dock = DockStyle.Top;
                    ////panel_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    /////

                    ////левая панель
                    //panel_left.Name = "panel_left" + count_for_controls.ToString();
                    //panel_left.Location = new Point(panel_up.Location.X, panel_up.Height);
                    //panel_left.Size = new Size(panel.Width / 4, panel.Width / 2);
                    ////panel_left.Dock = DockStyle.Left;
                    ////panel_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    ////панель для картинки
                    //panel_for_picture.Name = "panel_for_picture" + count_for_controls.ToString();
                    //panel_for_picture.Location = new Point(panel_left.Width, panel_up.Height);
                    //panel_for_picture.Size = new Size(panel.Width / 2, panel.Width / 2);
                    //panel_for_picture.Cursor = Cursors.Hand;
                    ////panel_for_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //// Initialize the picturebox control.    
                    //picture.Name = "picture" + count_for_controls.ToString();
                    //picture.Image = Properties.Resources.pc;
                    //picture.Width = panel_for_picture.Width;
                    //picture.Height = panel_for_picture.Width;
                    ////picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    ////picture.Margin = new Padding(50, 2, 50, 0);
                    //picture.SizeMode = PictureBoxSizeMode.Zoom;

                    ////////////////////////

                    //label.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height);
                    //label.Text = name;
                    //label.Name = "label_name" + count_for_controls;
                    //label.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //label.TextAlign = ContentAlignment.MiddleCenter;
                    //label.AutoSize = false;
                    ////label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //////
                    //dop_panel.Size = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //dop_panel.MaximumSize = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //dop_panel.MinimumSize = new Size(0, 0);
                    /////           

                    //label_ip.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height + label.Height);
                    //label_ip.Text = ip;
                    //label_ip.Name = "label_ip" + count_for_controls;
                    //label_ip.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //label_ip.TextAlign = ContentAlignment.MiddleCenter;
                    //label_ip.AutoSize = false;
                    ////label_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    //////////////

                    //label_up.Location = new Point(panel_up.Location.X, panel_up.Location.Y);
                    //label_up.Visible = false;
                    //label_up.Text = "false";
                    //label_up.Name = "label_status" + count_for_controls;
                    //label_up.Size = new Size(panel_up.Width, panel_up.Height);
                    //label_up.TextAlign = ContentAlignment.MiddleCenter;
                    //label_up.AutoSize = false;
                    ////label_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    /////


                    //flowLayoutPanel1.Controls.Add(panel);
                    //panel.Controls.Add(panel_up);
                    //panel.Controls.Add(panel_left);
                    //panel.Controls.Add(panel_for_picture);
                    //panel_for_picture.Controls.Add(picture);
                    //panel.Controls.Add(label);
                    //panel.Controls.Add(label_ip);
                    //panel_up.Controls.Add(label_up);

                    //////////////////
                    /////Добавляем обработчик
                    /////
                    ////panel.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////panel_up.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel_up.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////panel_left.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel_left.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////panel_for_picture.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel_for_picture.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////label.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////label.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////label_up.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////label_up.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////label_ip.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////label_ip.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);


                    //picture.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    //picture.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);
                    //eventChecker.setForm(this);

                });

            }
            else
            {
                try
                {
                    UserControl1 userControl1 = new UserControl1();
                    userControl1.ipPc = ip;
                    userControl1.namePc = name;
                    System.Windows.Forms.Integration.ElementHost elementHost = new System.Windows.Forms.Integration.ElementHost();
                    elementHost.Tag = "elementHost" + number_for_controls;
                    number_for_controls++;
                    elementHost.Height = elementHost1.Height - 30;
                    elementHost.Width = 120;
                    elementHost.Child = userControl1;
                    elementHost1.Child = userControl1;

                    //elementHost1.Child = elementHost;
                    //elementHost1.Controls.Add(elementHost);


                    //count_for_controls++;
                    //Panel panel = new Panel();
                    //Panel panel_up = new Panel();
                    //Panel panel_left = new Panel();
                    //Panel panel_for_picture = new Panel();
                    //Panel dop_panel = new Panel();

                    //PictureBox picture = new PictureBox();
                    //Label label = new Label();
                    //Label label_ip = new Label();
                    //Label label_up = new Label();

                    //// Initialize the Panel control.                    
                    //panel.Size = new Size(flowLayoutPanel1.Height, flowLayoutPanel1.Height - 30);
                    //panel.Name = "panel" + count_for_controls.ToString();                    
                    ////panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //// Set the Borderstyle for the Panel to three-dimensional.

                    /////
                    //panel_up.Name = "panel_up" + count_for_controls.ToString();
                    //panel_up.Size = new Size(panel.Width, panel.Width / 7);
                    //panel_up.MaximumSize = new Size(panel.Width, panel.Width / 7);
                    //panel_up.MinimumSize = new Size(panel.Width, 0);
                    ////panel_up.Dock = DockStyle.Top;
                    ////panel_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    /////

                    ////левая панель
                    //panel_left.Name = "panel_left" + count_for_controls.ToString();
                    //panel_left.Location = new Point(panel_up.Location.X, panel_up.Height);
                    //panel_left.Size = new Size(panel.Width / 4, panel.Width / 2);
                    ////panel_left.Dock = DockStyle.Left;
                    ////panel_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    ////панель для картинки
                    //panel_for_picture.Name = "panel_for_picture" + count_for_controls.ToString();
                    //panel_for_picture.Location = new Point(panel_left.Width, panel_up.Height);
                    //panel_for_picture.Size = new Size(panel.Width / 2, panel.Width / 2);
                    //panel_for_picture.Cursor = Cursors.Hand;
                    ////panel_for_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //// Initialize the picturebox control.    
                    //picture.Name = "picture" + count_for_controls.ToString();
                    //picture.Image = Properties.Resources.pc;
                    //picture.Width = panel_for_picture.Width;
                    //picture.Height = panel_for_picture.Width;
                    ////picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    ////picture.Margin = new Padding(50, 2, 50, 0);
                    //picture.SizeMode = PictureBoxSizeMode.Zoom;

                    ////////////////////////

                    //label.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height);
                    //label.Text = name;
                    //label.Name = "label_name" + count_for_controls;
                    //label.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //label.TextAlign = ContentAlignment.MiddleCenter;
                    //label.AutoSize = false;
                    ////label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //////
                    //dop_panel.Size = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //dop_panel.MaximumSize = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //dop_panel.MinimumSize = new Size(0, 0);
                    /////           

                    //label_ip.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height + label.Height);
                    //label_ip.Text = ip;
                    //label_ip.Name = "label_ip" + count_for_controls; 
                    //label_ip.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    //label_ip.TextAlign = ContentAlignment.MiddleCenter;
                    //label_ip.AutoSize = false;
                    ////label_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    //////////////

                    //label_up.Location = new Point(panel_up.Location.X, panel_up.Location.Y);
                    //label_up.Visible = false;
                    //label_up.Text = "false";
                    //label_up.Name = "label_status"+count_for_controls;
                    //label_up.Size = new Size(panel_up.Width, panel_up.Height);
                    //label_up.TextAlign = ContentAlignment.MiddleCenter;
                    //label_up.AutoSize = false;
                    ////label_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    /////


                    //flowLayoutPanel1.Controls.Add(panel);
                    //panel.Controls.Add(panel_up);
                    //panel.Controls.Add(panel_left);
                    //panel.Controls.Add(panel_for_picture);
                    //panel_for_picture.Controls.Add(picture);
                    //panel.Controls.Add(label);
                    //panel.Controls.Add(label_ip);
                    //panel_up.Controls.Add(label_up);

                    //////////////////
                    /////Добавляем обработчик
                    /////
                    ////panel.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////panel_up.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel_up.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////panel_left.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel_left.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////panel_for_picture.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////panel_for_picture.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////label.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////label.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////label_up.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////label_up.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);

                    ////label_ip.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    ////label_ip.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);


                    //picture.MouseEnter += new EventHandler(eventChecker.panel_MouseEnter);
                    //picture.MouseLeave += new EventHandler(eventChecker.panel_MouseLeave);
                    //eventChecker.setForm(this);

                }
                catch { }
            }

        }       
        public int cutting(string str)
        {
            int rez =0;
            int poz=0;
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (str[i].ToString() == j.ToString())
                    { 
                        poz = i;
                        break;
                    }
                        
                }
            }
            rez = Convert.ToInt32(str.Remove(0, poz));
            return rez;
        }
               
        
    }
}
