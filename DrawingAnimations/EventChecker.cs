using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace DrawingAnimations
{
    public class EventChecker
    {
        List<InformationAboutPanel> information = new List<InformationAboutPanel>();

        static public Form frm { get; set; }

        public void setForm(Form form)
        {
            frm = form;
        }
        static public int cutting(string str)
        {
            int rez = 0;
            int poz = 0;
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

        static public int fulling_size = 2;

        public void panel_MouseEnter(object sender, EventArgs e)
        {
            string number = "";
            //try
            //{
            //    Panel panel = (Panel)sender;
            //    number = panel.Name;
            //}
            //catch { }
            //try
            //{
            //    Label label = (Label)sender;
            //    number = label.Name;
            //}
            //catch { }
            try
            {
                PictureBox pictureBox = (PictureBox)sender;
                number = pictureBox.Name;
            }
            catch { }
            

            int num = cutting(number);

            string label_up = "label_status" + num;
            string flowLayoutPanel1 = "flowLayoutPanel1";
            string panel_up = "panel_up" + num;
            string panel_name = "panel" + num;

            foreach (var tb in frm.Controls[flowLayoutPanel1].Controls[panel_name].Controls[panel_up].Controls.OfType<Label>())
            {
                if (tb.Name == label_up && tb.Text != "true")
                {
                    if (tb.Name == label_up)
                    {
                        tb.Text = "true";
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();                        
                        timer.Interval = 1;
                        timer.Tick += new EventHandler(AddS);
                        timer.Tag = "timer" + num;
                        timer.Start();
                    }
                    
                }
            }

        }

        public void panel_MouseLeave(object sender, EventArgs e)
        {
            string number = "";
            //try
            //{
            //    Panel panel = (Panel)sender;
            //    number = panel.Name;
            //}
            //catch { }
            //try
            //{
            //    Label label = (Label)sender;
            //    number = label.Name;
            //}
            //catch { }
            try
            {
                PictureBox pictureBox = (PictureBox)sender;
                number = pictureBox.Name;
            }
            catch { }

            int num = cutting(number);
            string label_up = "label_status" + num;
            string flowLayoutPanel1 = "flowLayoutPanel1";
            string panel_up = "panel_up" + num;
            string panel_name = "panel" + num;
            foreach (var tb in frm.Controls[flowLayoutPanel1].Controls[panel_name].Controls[panel_up].Controls.OfType<Label>())
            {
                if (tb.Name == label_up)
                    tb.Text = "false";
            }
        }




        static void AddS(object obj,EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)obj;
            int number = cutting(timer.Tag.ToString());//Получаем цифру таймера

            string flowLayoutPanel1 = "flowLayoutPanel1";
            string panel = "panel" + number;
            string panel_up = "panel_up" + number;
            string panel_left = "panel_left" + number;
            string panel_for_picture = "panel_for_picture" + number;
            string picture = "picture" + number;
            string label_name = "label_name" + number;
            string label_ip = "label_ip" + number;
            string label_up = "label_status" + number;
            int x = fulling_size;
            if (frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Controls[label_up].Text == "true" && frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height > frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].MinimumSize.Height)
            {
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height -=  2*x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_left].Width -= x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Height += 2*x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Width += 2*x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Location = new System.Drawing.Point(frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Location.X-x, frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Location.Y-2*x);
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Controls[picture].Height += 2 * x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Controls[picture].Width += 2 * x;

                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Location = new System.Drawing.Point(frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Location.X - x, frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Location.Y -  x);
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Height +=  x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Width += 2 * x;

                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Location = new System.Drawing.Point(frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Location.X - x, frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Location.Y - x);
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Height += x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Width += 2 * x;


            }
            else if (frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Controls[label_up].Text == "false" && frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height < frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].MaximumSize.Height)
            {
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height += 2 * x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_left].Width += x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Height -= 2 * x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Width -= 2 * x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Location = new System.Drawing.Point(frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Location.X + x, frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Location.Y + 2 * x);
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Controls[picture].Height -= 2 * x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_for_picture].Controls[picture].Width -= 2 * x;

                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Location = new System.Drawing.Point(frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Location.X + x, frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Location.Y + x);
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Height -= x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_name].Width -= 2 * x;

                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Location = new System.Drawing.Point(frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Location.X + x, frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Location.Y + x);
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Height -= x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].Width -= 2 * x;
                frm.Controls[flowLayoutPanel1].Controls[panel].Controls[label_ip].BackColor = System.Drawing.Color.Transparent;
            }
            else if (frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].Height == frm.Controls[flowLayoutPanel1].Controls[panel].Controls[panel_up].MaximumSize.Height)
            {
                timer.Stop();
            }
           


        

        }
    }

    public class InformationAboutPanel
    {
        public int number { get; set; }

        public string flowLayoutPanel1 = "flowLayoutPanel1";
        public string panel = "panel";
        public string panel_up = "panel_up";
        public string panel_left = "panel_left";
        public string panel_for_picture = "panel_for_picture";
        public string picture = "picture";
        public string label_name = "label_name";
        public string label_ip = "label_ip";
        public string label_up = "label_status";

        public bool StatusMouse { get; set; }//False мыши нет на control

    }
}
