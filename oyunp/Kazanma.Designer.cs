namespace oyunp
{
    partial class Kazanma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kazanma));
            this.buttonMenu = new System.Windows.Forms.Button();
            this.buttonDevamet = new System.Windows.Forms.Button();
            this.lblZamanNick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonMenu
            // 
            this.buttonMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMenu.BackColor = System.Drawing.Color.Transparent;
            this.buttonMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMenu.BackgroundImage")));
            this.buttonMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonMenu.FlatAppearance.BorderSize = 0;
            this.buttonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenu.Location = new System.Drawing.Point(211, 354);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(174, 52);
            this.buttonMenu.TabIndex = 0;
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonDevamet
            // 
            this.buttonDevamet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDevamet.BackColor = System.Drawing.Color.Transparent;
            this.buttonDevamet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDevamet.BackgroundImage")));
            this.buttonDevamet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDevamet.FlatAppearance.BorderSize = 0;
            this.buttonDevamet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDevamet.Location = new System.Drawing.Point(414, 354);
            this.buttonDevamet.Name = "buttonDevamet";
            this.buttonDevamet.Size = new System.Drawing.Size(174, 52);
            this.buttonDevamet.TabIndex = 1;
            this.buttonDevamet.UseVisualStyleBackColor = false;
            this.buttonDevamet.Click += new System.EventHandler(this.buttonDevamet_Click);
            // 
            // lblZamanNick
            // 
            this.lblZamanNick.AutoSize = true;
            this.lblZamanNick.Location = new System.Drawing.Point(1143, 610);
            this.lblZamanNick.Name = "lblZamanNick";
            this.lblZamanNick.Size = new System.Drawing.Size(0, 16);
            this.lblZamanNick.TabIndex = 2;
            // 
            // Kazanma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.lblZamanNick);
            this.Controls.Add(this.buttonDevamet);
            this.Controls.Add(this.buttonMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Kazanma";
            this.Text = "Kazanma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.Button buttonDevamet;
        private System.Windows.Forms.Label lblZamanNick;
    }
}