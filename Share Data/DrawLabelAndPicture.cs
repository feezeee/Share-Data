using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share_Data
{
    class DrawLabelAndPicture
    {
        public void Drawing(string text,int x,int y,Form form)
        {
            Label label = new Label();
            label.Text = text;           
            label.AutoSize = true;
            label.Location = new Point(x, y);
            label.MaximumSize = new Size(400, 0);            
            form.Controls.Add(label);                        
        }//Добавляем label в указанную область 
    }
}
