using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Media;
using System.Windows.Forms;

namespace oyunp
{
    public partial class Form1 : Form
    {
        // --- DEĞİŞKENLER ---
        private Image orjinalResim;
        private Size orjinalBoyut;
        private Point orjinalKonum;
        private SoundPlayer sesOynatici;

        public Form1()
        {
            InitializeComponent();

            // --- TİTREME VE BEYAZ EKRAN ÇÖZÜMÜ ---
            // Bu kodlar arka plan resmini bozmadan titremeyi alır.
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            // -------------------------------------

            // BUTON AYARLARI
            if (basla1 != null)
            {
                basla1.SizeMode = PictureBoxSizeMode.StretchImage;
                basla1.Cursor = Cursors.Hand;
                basla1.BackColor = Color.Transparent;

                orjinalBoyut = basla1.Size;
                orjinalKonum = basla1.Location;

                if (basla1.Image != null)
                {
                    orjinalResim = basla1.Image;
                    YuvarlakYap(basla1);
                }
            }

            try { sesOynatici = new SoundPlayer("tiksesi.wav"); } catch { }

            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (basla1 != null && basla1.Size == orjinalBoyut)
            {
                orjinalKonum = basla1.Location;
            }
        }

        private void basla1_MouseEnter(object sender, EventArgs e)
        {
            if (orjinalResim == null) return;
            this.SuspendLayout();
            basla1.Image = ResmiParlat(orjinalResim, 1.30f);
            int buyume = 20;
            basla1.Size = new Size(orjinalBoyut.Width + buyume, orjinalBoyut.Height + buyume);
            basla1.Location = new Point(orjinalKonum.X - (buyume / 2), orjinalKonum.Y - (buyume / 2));
            YuvarlakYap(basla1);
            this.ResumeLayout(true);
        }

        private void basla1_MouseLeave(object sender, EventArgs e)
        {
            if (orjinalResim == null) return;
            this.SuspendLayout();
            basla1.Image = orjinalResim;
            basla1.Size = orjinalBoyut;
            basla1.Location = orjinalKonum;
            YuvarlakYap(basla1);
            this.ResumeLayout(true);
        }

        private void basla1_Click(object sender, EventArgs e)
        {
            try { sesOynatici.Play(); } catch { }

            oyunsec gecilecekEkran = new oyunsec();
            if (this.WindowState == FormWindowState.Maximized)
                gecilecekEkran.WindowState = FormWindowState.Maximized;
            else
            {
                gecilecekEkran.WindowState = FormWindowState.Normal;
                gecilecekEkran.StartPosition = FormStartPosition.Manual;
                gecilecekEkran.Location = this.Location;
            }
            gecilecekEkran.Show();
            this.Hide();
        }

        private void YuvarlakYap(PictureBox kutu)
        {
            using (GraphicsPath yol = new GraphicsPath())
            {
                yol.AddEllipse(0, 0, kutu.Width, kutu.Height);
                kutu.Region = new Region(yol);
            }
        }

        private Bitmap ResmiParlat(Image resim, float orani)
        {
            if (resim == null) return null;
            Bitmap parlak = new Bitmap(resim.Width, resim.Height);
            using (Graphics g = Graphics.FromImage(parlak))
            {
                ColorMatrix matris = new ColorMatrix(new float[][] {
                  new float[] { orani, 0, 0, 0, 0 },
                  new float[] { 0, orani, 0, 0, 0 },
                  new float[] { 0, 0, orani, 0, 0 },
                  new float[] { 0, 0, 0, 1, 0 },
                  new float[] { 0, 0, 0, 0, 1 }
                });
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorMatrix(matris);
                g.DrawImage(resim, new Rectangle(0, 0, resim.Width, resim.Height), 0, 0, resim.Width, resim.Height, GraphicsUnit.Pixel, attr);
            }
            return parlak;
        }

        private void Form1_Load(object sender, EventArgs e) { }
    }
}