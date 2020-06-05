using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share_Data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            

        }

        private void rounded_Button1_Click(object sender, EventArgs e)
        {
            int x=500, y=100;

            DrawLabelAndPicture draw = new DrawLabelAndPicture();

            List<string> ipAdress = new List<string>();//В лист заносим ip адреса

            FindIpAdress findIp = new FindIpAdress();


            ipAdress = findIp.FindAdress(); //Запускаем функцию поиска ip адресов

            //ipAdress.Add("Hello");

            for (int i=0;i<ipAdress.Count;i++)
            {
                
                draw.Drawing(ipAdress[i], x, y, this);//Добавляем ip Адреса в форму
            }
            
        }
    }
}
