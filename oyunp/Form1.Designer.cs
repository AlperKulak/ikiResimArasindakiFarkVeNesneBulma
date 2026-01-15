namespace oyunp
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.basla1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.basla1)).BeginInit();
            this.SuspendLayout();
            // 
            // basla1
            // 
            this.basla1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.basla1.BackColor = System.Drawing.Color.Transparent;
            this.basla1.Image = ((System.Drawing.Image)(resources.GetObject("basla1.Image")));
            this.basla1.Location = new System.Drawing.Point(404, 156);
            this.basla1.Name = "basla1";
            this.basla1.Size = new System.Drawing.Size(356, 271);
            this.basla1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.basla1.TabIndex = 2;
            this.basla1.TabStop = false;
            this.basla1.Click += new System.EventHandler(this.basla1_Click);
            this.basla1.MouseEnter += new System.EventHandler(this.basla1_MouseEnter);
            this.basla1.MouseLeave += new System.EventHandler(this.basla1_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1168, 559);
            this.Controls.Add(this.basla1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.basla1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox basla1;
    }
}

