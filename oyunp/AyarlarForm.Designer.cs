namespace oyunp
{
    partial class AyarlarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AyarlarForm));
            this.btnOyundanCik = new System.Windows.Forms.PictureBox();
            this.btnGeriDon = new System.Windows.Forms.PictureBox();
            this.btnMuzik = new System.Windows.Forms.PictureBox();
            this.lblSes = new System.Windows.Forms.Label();
            this.trackSes = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.btnOyundanCik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGeriDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMuzik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOyundanCik
            // 
            this.btnOyundanCik.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOyundanCik.Image = ((System.Drawing.Image)(resources.GetObject("btnOyundanCik.Image")));
            this.btnOyundanCik.Location = new System.Drawing.Point(922, 12);
            this.btnOyundanCik.Name = "btnOyundanCik";
            this.btnOyundanCik.Size = new System.Drawing.Size(200, 152);
            this.btnOyundanCik.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOyundanCik.TabIndex = 0;
            this.btnOyundanCik.TabStop = false;
            this.btnOyundanCik.Click += new System.EventHandler(this.btnOyundanCik_Click);
            this.btnOyundanCik.MouseEnter += new System.EventHandler(this.btnOyundanCik_MouseEnter);
            this.btnOyundanCik.MouseLeave += new System.EventHandler(this.btnOyundanCik_MouseLeave);
            // 
            // btnGeriDon
            // 
            this.btnGeriDon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGeriDon.Image = ((System.Drawing.Image)(resources.GetObject("btnGeriDon.Image")));
            this.btnGeriDon.Location = new System.Drawing.Point(922, 196);
            this.btnGeriDon.Name = "btnGeriDon";
            this.btnGeriDon.Size = new System.Drawing.Size(200, 152);
            this.btnGeriDon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnGeriDon.TabIndex = 1;
            this.btnGeriDon.TabStop = false;
            this.btnGeriDon.Click += new System.EventHandler(this.btnGeriDon_Click);
            this.btnGeriDon.MouseEnter += new System.EventHandler(this.btnGeriDon_MouseEnter);
            this.btnGeriDon.MouseLeave += new System.EventHandler(this.btnGeriDon_MouseLeave);
            // 
            // btnMuzik
            // 
            this.btnMuzik.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMuzik.Image = ((System.Drawing.Image)(resources.GetObject("btnMuzik.Image")));
            this.btnMuzik.Location = new System.Drawing.Point(922, 377);
            this.btnMuzik.Name = "btnMuzik";
            this.btnMuzik.Size = new System.Drawing.Size(200, 152);
            this.btnMuzik.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMuzik.TabIndex = 3;
            this.btnMuzik.TabStop = false;
            this.btnMuzik.Click += new System.EventHandler(this.btnMuzik_Click);
            this.btnMuzik.MouseEnter += new System.EventHandler(this.btnMuzik_MouseEnter);
            this.btnMuzik.MouseLeave += new System.EventHandler(this.btnMuzik_MouseLeave);
            // 
            // lblSes
            // 
            this.lblSes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSes.BackColor = System.Drawing.Color.Transparent;
            this.lblSes.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSes.Location = new System.Drawing.Point(990, 546);
            this.lblSes.Name = "lblSes";
            this.lblSes.Size = new System.Drawing.Size(62, 32);
            this.lblSes.TabIndex = 4;
            this.lblSes.Text = "100";
            this.lblSes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSes.Visible = false;
            // 
            // trackSes
            // 
            this.trackSes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.trackSes.LargeChange = 100;
            this.trackSes.Location = new System.Drawing.Point(910, 590);
            this.trackSes.Maximum = 100;
            this.trackSes.Name = "trackSes";
            this.trackSes.Size = new System.Drawing.Size(235, 56);
            this.trackSes.TabIndex = 5;
            this.trackSes.TickFrequency = 5;
            this.trackSes.Visible = false;
            this.trackSes.Scroll += new System.EventHandler(this.trackSes_Scroll);
            this.trackSes.ValueChanged += new System.EventHandler(this.trackSes_ValueChanged);
            // 
            // AyarlarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1222, 658);
            this.Controls.Add(this.trackSes);
            this.Controls.Add(this.lblSes);
            this.Controls.Add(this.btnMuzik);
            this.Controls.Add(this.btnGeriDon);
            this.Controls.Add(this.btnOyundanCik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AyarlarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AyarlarForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.btnOyundanCik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGeriDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMuzik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnOyundanCik;
        private System.Windows.Forms.PictureBox btnGeriDon;
        private System.Windows.Forms.PictureBox btnMuzik;
        private System.Windows.Forms.Label lblSes;
        private System.Windows.Forms.TrackBar trackSes;
    }
}