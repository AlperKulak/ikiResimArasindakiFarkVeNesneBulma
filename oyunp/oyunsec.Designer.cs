namespace oyunp
{
    partial class oyunsec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(oyunsec));
            this.pictureBoxSol = new System.Windows.Forms.PictureBox();
            this.pictureBoxSag = new System.Windows.Forms.PictureBox();
            this.pictureBoxAyarlar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAyarlar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSol
            // 
            this.pictureBoxSol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxSol.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSol.Image")));
            this.pictureBoxSol.Location = new System.Drawing.Point(118, 198);
            this.pictureBoxSol.Name = "pictureBoxSol";
            this.pictureBoxSol.Size = new System.Drawing.Size(225, 130);
            this.pictureBoxSol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSol.TabIndex = 0;
            this.pictureBoxSol.TabStop = false;
            this.pictureBoxSol.Click += new System.EventHandler(this.pictureBoxSol_Click);
            this.pictureBoxSol.MouseEnter += new System.EventHandler(this.pictureBoxSol_MouseEnter);
            this.pictureBoxSol.MouseLeave += new System.EventHandler(this.pictureBoxSol_MouseLeave);
            // 
            // pictureBoxSag
            // 
            this.pictureBoxSag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxSag.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSag.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSag.Image")));
            this.pictureBoxSag.Location = new System.Drawing.Point(595, 198);
            this.pictureBoxSag.Name = "pictureBoxSag";
            this.pictureBoxSag.Size = new System.Drawing.Size(224, 130);
            this.pictureBoxSag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSag.TabIndex = 1;
            this.pictureBoxSag.TabStop = false;
            this.pictureBoxSag.Click += new System.EventHandler(this.pictureBoxSag_Click);
            this.pictureBoxSag.MouseEnter += new System.EventHandler(this.pictureBoxSag_MouseEnter);
            this.pictureBoxSag.MouseLeave += new System.EventHandler(this.pictureBoxSag_MouseLeave);
            // 
            // pictureBoxAyarlar
            // 
            this.pictureBoxAyarlar.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAyarlar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxAyarlar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxAyarlar.Image")));
            this.pictureBoxAyarlar.Location = new System.Drawing.Point(399, 445);
            this.pictureBoxAyarlar.Name = "pictureBoxAyarlar";
            this.pictureBoxAyarlar.Size = new System.Drawing.Size(103, 77);
            this.pictureBoxAyarlar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAyarlar.TabIndex = 2;
            this.pictureBoxAyarlar.TabStop = false;
            this.pictureBoxAyarlar.Click += new System.EventHandler(this.pictureBoxAyarlar_Click);
            this.pictureBoxAyarlar.MouseEnter += new System.EventHandler(this.pictureBoxAyarlar_MouseEnter);
            this.pictureBoxAyarlar.MouseLeave += new System.EventHandler(this.pictureBoxAyarlar_MouseLeave);
            // 
            // oyunsec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(939, 525);
            this.Controls.Add(this.pictureBoxAyarlar);
            this.Controls.Add(this.pictureBoxSag);
            this.Controls.Add(this.pictureBoxSol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "oyunsec";
            this.Text = "oyunsec";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.oyunsec_FormClosed);
            this.Load += new System.EventHandler(this.oyunsec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAyarlar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSol;
        private System.Windows.Forms.PictureBox pictureBoxSag;
        private System.Windows.Forms.PictureBox pictureBoxAyarlar;
    }
}