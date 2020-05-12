using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share_Data
{
    public class Rounded_Button : Button
    {
        #region свойства    
        private int roundingPercent = 100;
        [DisplayName("rounding [%]")]
        [DefaultValue(100)]
        [Description("Указывает радиус акругления в процентах")]        
        public int Rounding
        {
            get => roundingPercent;
            set
            {
                if(value>=0&&value<=100)
                {
                    roundingPercent = value;
                    Refresh();
                }
            }
        }



        #endregion
        GraphicsPath GetRoundPath(RectangleF Rect, float radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();

            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                                Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);

            //Расчет радиуса закругления
            float roundingValue = 0.1F;
            if(roundingPercent>0)
            {
                roundingValue = Height / 100F * roundingPercent;
            }

            GraphicsPath GraphPath = GetRoundPath(Rect, roundingValue);

            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(Color.Lime, 1.75f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
                graphics.SetClip(Rect);                
            }
        }
    }
}
