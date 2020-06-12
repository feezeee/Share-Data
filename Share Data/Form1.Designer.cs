namespace Share_Data
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_for_right = new System.Windows.Forms.Timer(this.components);
            this.timer_for_left = new System.Windows.Forms.Timer(this.components);
            this.rounded_Button1 = new Share_Data.Rounded_Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new WpfControlLibrary2.UserControl1();
            this.SuspendLayout();
            // 
            // rounded_Button1
            // 
            this.rounded_Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rounded_Button1.Location = new System.Drawing.Point(228, 320);
            this.rounded_Button1.Name = "rounded_Button1";
            this.rounded_Button1.Size = new System.Drawing.Size(298, 59);
            this.rounded_Button1.TabIndex = 0;
            this.rounded_Button1.Text = "rounded_Button1";
            this.rounded_Button1.UseVisualStyleBackColor = true;
            this.rounded_Button1.Click += new System.EventHandler(this.rounded_Button1_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(12, 81);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(740, 220);
            this.elementHost1.TabIndex = 1;
            this.elementHost1.Tag = "";
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.userControl11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(764, 450);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.rounded_Button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Rounded_Button rounded_Button1;
        private System.Windows.Forms.Timer timer_for_right;
        private System.Windows.Forms.Timer timer_for_left;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private WpfControlLibrary2.UserControl1 userControl11;
    }
}

