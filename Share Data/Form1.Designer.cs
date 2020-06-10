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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox_for_left = new System.Windows.Forms.PictureBox();
            this.pictureBox_for_right = new System.Windows.Forms.PictureBox();
            this.timer_for_right = new System.Windows.Forms.Timer(this.components);
            this.timer_for_left = new System.Windows.Forms.Timer(this.components);
            this.rounded_Button1 = new Share_Data.Rounded_Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_for_left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_for_right)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(39, 59);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(686, 220);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox_for_left
            // 
            this.pictureBox_for_left.Image = global::Share_Data.Properties.Resources.left;
            this.pictureBox_for_left.Location = new System.Drawing.Point(12, 59);
            this.pictureBox_for_left.Name = "pictureBox_for_left";
            this.pictureBox_for_left.Size = new System.Drawing.Size(21, 220);
            this.pictureBox_for_left.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_for_left.TabIndex = 3;
            this.pictureBox_for_left.TabStop = false;
            this.pictureBox_for_left.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
            this.pictureBox_for_left.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            // 
            // pictureBox_for_right
            // 
            this.pictureBox_for_right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_for_right.Image = global::Share_Data.Properties.Resources.right;
            this.pictureBox_for_right.Location = new System.Drawing.Point(731, 59);
            this.pictureBox_for_right.Name = "pictureBox_for_right";
            this.pictureBox_for_right.Size = new System.Drawing.Size(21, 220);
            this.pictureBox_for_right.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_for_right.TabIndex = 2;
            this.pictureBox_for_right.TabStop = false;
            this.pictureBox_for_right.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox_for_right.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // timer_for_right
            // 
            this.timer_for_right.Interval = 10;
            this.timer_for_right.Tag = "timer_for_right";
            this.timer_for_right.Tick += new System.EventHandler(this.timer_for_right_Tick);
            // 
            // timer_for_left
            // 
            this.timer_for_left.Interval = 10;
            this.timer_for_left.Tag = "timer_for_left";
            this.timer_for_left.Tick += new System.EventHandler(this.timer_for_left_Tick);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(764, 450);
            this.Controls.Add(this.pictureBox_for_left);
            this.Controls.Add(this.pictureBox_for_right);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.rounded_Button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_for_left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_for_right)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Rounded_Button rounded_Button1;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox_for_right;
        private System.Windows.Forms.PictureBox pictureBox_for_left;
        private System.Windows.Forms.Timer timer_for_right;
        private System.Windows.Forms.Timer timer_for_left;
    }
}

