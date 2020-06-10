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
            frm = this;

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
        /// <summary>
        /// Добавляет картинку пк на форму
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="form"></param>
        /// 

        public int count_for_controls = 0;
        public void Drawing_picture_for_pc(Form form, string name, string ip)
        {
            if (this.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {

                    count_for_controls++;
                    Panel panel = new Panel();
                    Panel panel_up = new Panel();
                    Panel panel_left = new Panel();
                    Panel panel_for_picture = new Panel();
                    Panel dop_panel = new Panel();

                    PictureBox picture = new PictureBox();
                    Label label = new Label();
                    Label label_ip = new Label();
                    Label label_up = new Label();

                    // Initialize the Panel control.                    
                    panel.Size = new Size(flowLayoutPanel1.Height - 10, flowLayoutPanel1.Height - 10);
                    panel.Name = "panel" + count_for_controls.ToString();
                    panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    // Set the Borderstyle for the Panel to three-dimensional.

                    ///
                    panel_up.Name = "panel_up" + count_for_controls.ToString();
                    panel_up.Size = new Size(panel.Width, panel.Width / 7);
                    panel_up.MaximumSize = new Size(panel.Width, panel.Width / 7);
                    panel_up.MinimumSize = new Size(panel.Width, 0);
                    panel_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    ///

                    //левая панель
                    panel_left.Name = "panel_left" + count_for_controls.ToString();
                    panel_left.Location = new Point(panel_up.Location.X, panel_up.Height);
                    panel_left.Size = new Size(panel.Width / 4, panel.Width / 2);
                    panel_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //панель для картинки
                    panel_for_picture.Name = "panel_for_picture" + count_for_controls.ToString();
                    panel_for_picture.Location = new Point(panel_left.Width, panel_up.Height);
                    panel_for_picture.Size = new Size(panel.Width / 2, panel.Width / 2);
                    panel_for_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    // Initialize the picturebox control.    
                    picture.Name = "picture" + count_for_controls.ToString();
                    picture.Image = Properties.Resources.pc;
                    picture.Width = panel_for_picture.Width;
                    picture.Height = panel_for_picture.Width;
                    picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    //picture.Margin = new Padding(50, 2, 50, 0);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;

                    //////////////////////

                    label.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height);
                    label.Text = name;
                    label.Name = name;
                    label.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.AutoSize = false;
                    label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    ////
                    dop_panel.Size = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    dop_panel.MaximumSize = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    dop_panel.MinimumSize = new Size(0, 0);
                    ///           

                    label_ip.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height + label.Height);
                    label_ip.Text = ip;
                    label_ip.Name = ip;
                    label_ip.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    label_ip.TextAlign = ContentAlignment.MiddleCenter;
                    label_ip.AutoSize = false;
                    label_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    ////////////

                    label_up.Location = new Point(panel_up.Location.X, panel_up.Location.Y);
                    label_up.Text = "false";
                    label_up.Name = "label_status" + count_for_controls;
                    label_up.Size = new Size(panel_up.Width, panel_up.Height);
                    label_up.TextAlign = ContentAlignment.MiddleCenter;
                    label_up.AutoSize = false;
                    label_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    ///


                    flowLayoutPanel1.Controls.Add(panel);
                    panel.Controls.Add(panel_up);
                    panel.Controls.Add(panel_left);
                    panel.Controls.Add(panel_for_picture);
                    panel_for_picture.Controls.Add(picture);
                    panel.Controls.Add(label);
                    panel.Controls.Add(label_ip);
                    panel_up.Controls.Add(label_up);

                    ////////////////
                    ///Добавляем обработчик
                    ///
                    panel.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel.MouseLeave += new EventHandler(panel_MouseLeave);

                    panel_up.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel_up.MouseLeave += new EventHandler(panel_MouseLeave);

                    panel_left.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel_left.MouseLeave += new EventHandler(panel_MouseLeave);

                    panel_for_picture.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel_for_picture.MouseLeave += new EventHandler(panel_MouseLeave);

                    label.MouseEnter += new EventHandler(panel_MouseEnter);
                    label.MouseLeave += new EventHandler(panel_MouseLeave);

                    label_up.MouseEnter += new EventHandler(panel_MouseEnter);
                    label_up.MouseLeave += new EventHandler(panel_MouseLeave);

                    label_ip.MouseEnter += new EventHandler(panel_MouseEnter);
                    label_ip.MouseLeave += new EventHandler(panel_MouseLeave);


                    picture.MouseEnter += new EventHandler(panel_MouseEnter);
                    picture.MouseLeave += new EventHandler(panel_MouseLeave);


                });

            }
            else
            {
                try
                {
                    count_for_controls++;
                    Panel panel = new Panel();
                    Panel panel_up = new Panel();
                    Panel panel_left = new Panel();
                    Panel panel_for_picture = new Panel();
                    Panel dop_panel = new Panel();

                    PictureBox picture = new PictureBox();
                    Label label = new Label();
                    Label label_ip = new Label();
                    Label label_up = new Label();

                    // Initialize the Panel control.                    
                    panel.Size = new Size(flowLayoutPanel1.Height - 10, flowLayoutPanel1.Height - 10);
                    panel.Name = "panel" + count_for_controls.ToString();                    
                    panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    // Set the Borderstyle for the Panel to three-dimensional.

                    ///
                    panel_up.Name = "panel_up" + count_for_controls.ToString();
                    panel_up.Size = new Size(panel.Width, panel.Width / 7);
                    panel_up.MaximumSize = new Size(panel.Width, panel.Width / 7);
                    panel_up.MinimumSize = new Size(panel.Width, 0);
                    panel_up.Dock = DockStyle.Top;
                    panel_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    ///

                    //левая панель
                    panel_left.Name = "panel_left" + count_for_controls.ToString();
                    panel_left.Location = new Point(panel_up.Location.X, panel_up.Height);
                    panel_left.Size = new Size(panel.Width / 4, panel.Width / 2);
                    panel_left.Dock = DockStyle.Left;
                    panel_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    //панель для картинки
                    panel_for_picture.Name = "panel_for_picture" + count_for_controls.ToString();
                    panel_for_picture.Location = new Point(panel_left.Width, panel_up.Height);
                    panel_for_picture.Size = new Size(panel.Width / 2, panel.Width / 2);
                    panel_for_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    // Initialize the picturebox control.    
                    picture.Name = "picture" + count_for_controls.ToString();
                    picture.Image = Properties.Resources.pc;
                    picture.Width = panel_for_picture.Width;
                    picture.Height = panel_for_picture.Width;
                    picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    //picture.Margin = new Padding(50, 2, 50, 0);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;

                    //////////////////////

                    label.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height);
                    label.Text = name;
                    label.Name = "label_name" + count_for_controls;
                    label.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.AutoSize = false;
                    label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    ////
                    dop_panel.Size = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    dop_panel.MaximumSize = new Size(panel.Width, (panel.Width - (panel_up.Height + panel_for_picture.Height)) / 2);
                    dop_panel.MinimumSize = new Size(0, 0);
                    ///           

                    label_ip.Location = new Point(panel_for_picture.Location.X, panel_up.Height + panel_for_picture.Height + label.Height);
                    label_ip.Text = ip;
                    label_ip.Name = "label_ip" + count_for_controls; 
                    label_ip.Size = new Size(panel_for_picture.Width, (panel.Height - (panel_up.Height + panel_for_picture.Height)) / 2);
                    label_ip.TextAlign = ContentAlignment.MiddleCenter;
                    label_ip.AutoSize = false;
                    label_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    ////////////

                    label_up.Location = new Point(panel_up.Location.X, panel_up.Location.Y);
                    label_up.Text = "false";
                    label_up.Name = "label_status"+count_for_controls;
                    label_up.Size = new Size(panel_up.Width, panel_up.Height);
                    label_up.TextAlign = ContentAlignment.MiddleCenter;
                    label_up.AutoSize = false;
                    label_up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    ///


                    flowLayoutPanel1.Controls.Add(panel);
                    panel.Controls.Add(panel_up);
                    panel.Controls.Add(panel_left);
                    panel.Controls.Add(panel_for_picture);
                    panel_for_picture.Controls.Add(picture);
                    panel.Controls.Add(label);
                    panel.Controls.Add(label_ip);
                    panel_up.Controls.Add(label_up);

                    ////////////////
                    ///Добавляем обработчик
                    ///
                    panel.MouseEnter += new EventHandler(panel_MouseEnter);                         
                    panel.MouseLeave += new EventHandler(panel_MouseLeave);

                    panel_up.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel_up.MouseLeave += new EventHandler(panel_MouseLeave);

                    panel_left.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel_left.MouseLeave += new EventHandler(panel_MouseLeave);

                    panel_for_picture.MouseEnter += new EventHandler(panel_MouseEnter);
                    panel_for_picture.MouseLeave += new EventHandler(panel_MouseLeave);

                    label.MouseEnter += new EventHandler(panel_MouseEnter);
                    label.MouseLeave += new EventHandler(panel_MouseLeave);

                    label_up.MouseEnter += new EventHandler(panel_MouseEnter);
                    label_up.MouseLeave += new EventHandler(panel_MouseLeave);

                    label_ip.MouseEnter += new EventHandler(panel_MouseEnter);
                    label_ip.MouseLeave += new EventHandler(panel_MouseLeave);


                    picture.MouseEnter += new EventHandler(panel_MouseEnter);
                    picture.MouseLeave += new EventHandler(panel_MouseLeave);


                }
                catch { }
            }

        }

        List<object> sendering;

        private string name;

        private bool mouse_status = false;
        private bool min_status = true;
        private bool max_status = false;

        Form frm;
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

        public int fulling_size = 2;
        private void panel_MouseEnter(object sender, EventArgs e)
        {
            string number="";
            try 
            {
                Panel panel = (Panel)sender;
                number = panel.Name; 
            }
            catch { }
            try
            {
                Label label = (Label)sender;
                number = label.Name;
            }
            catch { }
            try
            {
                PictureBox pictureBox = (PictureBox)sender;
                number = pictureBox.Name;
            }
            catch { }


            int num = cutting(number);
            string label_up = "label_status" + num;
            string panel_up = "panel_up" + num;
            string panel_name = "panel" + num;
            //AddS(number, fulling_size, frm, _PCs[Convert.ToInt32(number) - 1].Name, _PCs[Convert.ToInt32(number) - 1].Adress);
            //panel.Controls.Find(label_up);
            foreach (var tb in flowLayoutPanel1.Controls[panel_name].Controls[panel_up].Controls.OfType<Label>())
            {
                if (tb.Name == label_up && tb.Text != "true")
                {
                    //await Task.Run(() => AddS(num, fulling_size, frm));
                    tb.Text = "true";
                    //Thread add = new Thread(new ParameterizedThreadStart(AddS));
                    var addS = new AddSStruct
                    {
                        number = num,
                        x = fulling_size,
                        form = frm
                    };
                    //add.Start(addS);
                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Interval = 100;
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(addS.TimerTick);
                    timer.AutoReset = true;
                    //timer.Start();

                    //AddS(num, fulling_size, frm);
                }
            }        
            
        }

        private void panel_MouseLeave(object sender, EventArgs e)
        {
            string number = "";
            try
            {
                Panel panel = (Panel)sender;
                number = panel.Name;
            }
            catch { }
            try
            {
                Label label = (Label)sender;
                number = label.Name;
            }
            catch { }
            try
            {
                PictureBox pictureBox = (PictureBox)sender;
                number = pictureBox.Name;
            }
            catch { }

            int num = cutting(number);
            string label_up = "label_status" + num;
            string panel_up = "panel_up" + num;
            string panel_name = "panel" + num;
            foreach (var tb in flowLayoutPanel1.Controls[panel_name].Controls[panel_up].Controls.OfType<Label>())
            {
                if (tb.Name == label_up)
                    tb.Text = "false";
            }
        }

        struct AddSStruct
        {
            public int number { get; set; }
            public int x { get; set; }
            public Form form { get; set; }
            public void TimerTick(object sender, ElapsedEventArgs e)
            {
                AddS(number, x, form);
            }
        }

        static void AddS(int number, int x, Form form)
        {
            //if (obj.GetType() != typeof(AddSStruct))
            //    return;
            //AddSStruct ps = (AddSStruct)obj;
            //int number = ps.number;
            //int x = ps.x;
            //Form form = ps.form;
            //if (this.InvokeRequired)
            //{
            //    Invoke((MethodInvoker)delegate ()
            //    {
                    try
                    {
                        string flowLayoutPanel1 = "flowLayoutPanel1";
                        string panel = "panel" + number;
                        string panel_up = "panel_up" + number;
                        string panel_left = "panel_left" + number;
                        string panel_for_picture = "panel_for_picture" + number;
                        string picture = "picture" + number;
                        string label_name = "label_name" + number;
                        string label_ip = "label_ip" + number;
                        string label_up = "label_status" + number;


                        bool flag = true;

                        while (flag)
                        {

                            try
                            {


                                if (form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Controls[label_up].Text == "true" && form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height > form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].MinimumSize.Height)
                                {
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height -= 2 * x;
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_left].Width -= x;
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Height += 2 * x;
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Width += 2 * x;
                                }
                                if (form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Controls[label_up].Text == "false" && form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height < form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].MaximumSize.Height)
                                {
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height += 2 * x;
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_left].Width += x;
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Height -= 2 * x;
                                    form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Width -= 2 * x;
                                }
                                if (form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height == form.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].MaximumSize.Height)
                                {
                                    flag = false;
                                }
                                else
                                    Thread.Sleep(100);
                            }
                            catch { }

                        }
                        //form.Controls[panel_up].;
                    }
                    catch { }
            //    });

            //}

        }
    
    }
}
