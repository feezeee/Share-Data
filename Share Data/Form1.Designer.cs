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
            this.rounded_Button1 = new Share_Data.Rounded_Button();
            this.SuspendLayout();
            // 
            // rounded_Button1
            // 
            this.rounded_Button1.Location = new System.Drawing.Point(240, 172);
            this.rounded_Button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rounded_Button1.Name = "rounded_Button1";
            this.rounded_Button1.Size = new System.Drawing.Size(520, 128);
            this.rounded_Button1.TabIndex = 0;
            this.rounded_Button1.Text = "rounded_Button1";
            this.rounded_Button1.UseVisualStyleBackColor = true;
            this.rounded_Button1.Click += new System.EventHandler(this.rounded_Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.rounded_Button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Rounded_Button rounded_Button1;
    }
}

