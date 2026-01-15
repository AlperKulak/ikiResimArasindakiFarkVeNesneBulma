namespace oyunp
{
    partial class Kayip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kayip));
            this.buttonMenu = new System.Windows.Forms.Button();
            this.buttonYeniden = new System.Windows.Forms.Button();
            this.lblZamanNick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.Transparent;
            this.buttonMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMenu.BackgroundImage")));
            this.buttonMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonMenu.FlatAppearance.BorderSize = 0;
            this.buttonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenu.Location = new System.Drawing.Point(210, 353);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(181, 64);
            this.buttonMenu.TabIndex = 0;
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonYeniden
            // 
            this.buttonYeniden.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonYeniden.BackColor = System.Drawing.Color.Transparent;
            this.buttonYeniden.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonYeniden.BackgroundImage")));
            this.buttonYeniden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonYeniden.FlatAppearance.BorderSize = 0;
            this.buttonYeniden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonYeniden.Location = new System.Drawing.Point(411, 353);
            this.buttonYeniden.Name = "buttonYeniden";
            this.buttonYeniden.Size = new System.Drawing.Size(188, 64);
            this.buttonYeniden.TabIndex = 1;
            this.buttonYeniden.UseVisualStyleBackColor = false;
            this.buttonYeniden.Click += new System.EventHandler(this.buttonYeniden_Click);
            // 
            // lblZamanNick
            // 
            this.lblZamanNick.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblZamanNick.AutoSize = true;
            this.lblZamanNick.Location = new System.Drawing.Point(1361, 674);
            this.lblZamanNick.Name = "lblZamanNick";
            this.lblZamanNick.Size = new System.Drawing.Size(44, 16);
            this.lblZamanNick.TabIndex = 2;
            this.lblZamanNick.Text = "label1";
            // 
            // Kayip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.lblZamanNick);
            this.Controls.Add(this.buttonYeniden);
            this.Controls.Add(this.buttonMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Kayip";
            this.Text = "Kayip";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.Button buttonYeniden;
        private System.Windows.Forms.Label lblZamanNick;
    }
}