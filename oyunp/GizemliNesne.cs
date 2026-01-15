using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace oyunp
{
    public partial class GizemliNesne : Form
    {
        private float refGenislik = 1920f;
        private float refYukseklik = 1080f;
        private int orjBtnW = 260;
        private int orjBtnH = 200;
        private int ayarBtnW = 100;
        private int ayarBtnH = 100;
        private int kaydirmaX = 100;
        private int kaydirmaY = 50;

        public string oyuncuIsmi;
        SoundPlayer tikSesi;

        // DEĞİŞTİ: Yerel WMP silindi (MuzikYoneticisi kullanılıyor).

        private List<Point> hedefNoktalar = new List<Point>()
        {
            new Point(540, 270), new Point(839, 270), new Point(1138, 270),
            new Point(540, 480), new Point(839, 480), new Point(1138, 480),
            new Point(540, 690), new Point(839, 690), new Point(1138, 690)
        };

        public GizemliNesne(string gelenIsim)
        {
            InitializeComponent();
            oyuncuIsmi = string.IsNullOrEmpty(gelenIsim) ? "Misafir" : gelenIsim;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            try { tikSesi = new SoundPlayer(Properties.Resources.tiksesi); } catch { }

            // DEĞİŞTİ: Müzik yöneticiden başlatılıyor.
            MuzikYoneticisi.Baslat();

            this.Load += GizemliNesne_Load;
            this.Resize += GizemliNesne_Resize;
        }

        public GizemliNesne() : this("Misafir") { }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void GizemliNesne_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            ElemanlariYerlestirVeBagla();
        }

        private void GizemliNesne_Resize(object sender, EventArgs e) { ElemanlariYerlestirVeBagla(); }

        private void ElemanlariYerlestirVeBagla()
        {
            if (this.ClientSize.Width == 0 || this.ClientSize.Height == 0) return;
            float oranX = this.ClientSize.Width / refGenislik;
            float oranY = this.ClientSize.Height / refYukseklik;
            int yeniW = (int)(orjBtnW * oranX);
            int yeniH = (int)(orjBtnH * oranY);

            for (int i = 0; i < hedefNoktalar.Count; i++)
            {
                string butonAdi = "buttonOyun" + (i + 1);
                Control btn = this.Controls[butonAdi];
                if (btn != null)
                {
                    ButonStiliUygula(btn);
                    float hedefX = hedefNoktalar[i].X * oranX;
                    float hedefY = hedefNoktalar[i].Y * oranY;
                    int sonX = (int)(hedefX - (yeniW / 2)) + kaydirmaX;
                    int sonY = (int)(hedefY - (yeniH / 2)) + kaydirmaY;
                    btn.Size = new Size(yeniW, yeniH);
                    btn.Location = new Point(sonX, sonY);
                    btn.BringToFront();
                    btn.Click -= GenelOyunButonu_Click;
                    btn.Click += GenelOyunButonu_Click;
                    btn.Tag = (i + 1);
                }
            }

            Control btnAyar = this.Controls["buttonAyarlar"];
            if (btnAyar != null)
            {
                ButonStiliUygula(btnAyar);
                int yeniAyarW = (int)(ayarBtnW * oranX);
                int yeniAyarH = (int)(ayarBtnH * oranY);
                int ayarX = this.ClientSize.Width - yeniAyarW - (int)(50 * oranX);
                int ayarY = (int)(50 * oranY);
                btnAyar.Size = new Size(yeniAyarW, yeniAyarH);
                btnAyar.Location = new Point(ayarX, ayarY);
                btnAyar.BringToFront();
                btnAyar.Click -= buttonAyarlar_Click;
                btnAyar.Click += buttonAyarlar_Click;
            }
        }

        private void ButonStiliUygula(Control btn)
        {
            if (btn is Button b)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.BackColor = Color.Transparent;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 0, 0, 0);
                b.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
                b.Text = "";
                b.Cursor = Cursors.Hand;
            }
        }

        private void TikSesiCal() { try { tikSesi.Play(); } catch { } }

        private void GenelOyunButonu_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            Button tiklananButon = (Button)sender;
            int butonNo = Convert.ToInt32(tiklananButon.Tag);

            if (butonNo == 1)
            {
                // DEĞİŞTİ: Bölüme geçerken menü müziği durduruluyor.
                MuzikYoneticisi.Durdur();

                GizemliNesne1 bolum1 = new GizemliNesne1(oyuncuIsmi);
                if (this.WindowState == FormWindowState.Maximized) bolum1.WindowState = FormWindowState.Maximized;
                bolum1.Show();
                this.Close();
            }
            else { MessageBox.Show(butonNo + ". Bölüm henüz aktif değil!"); }
        }

        private void buttonAyarlar_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            // DEĞİŞTİ: Ses ayarı yönetici üzerinden yapılıyor.
            int suankiSes = MuzikYoneticisi.SesSeviyesiniGetir();
            AyarlarForm ayarlar = new AyarlarForm(suankiSes, (yeniSes) => { MuzikYoneticisi.SesAyarla(yeniSes); });
            ayarlar.ShowDialog();
        }

        // Hata Önleyiciler
        private void buttonOyun1_Click(object sender, EventArgs e) { }
        private void buttonOyun2_Click(object sender, EventArgs e) { }
        private void buttonOyun3_Click(object sender, EventArgs e) { }
        private void buttonOyun4_Click(object sender, EventArgs e) { }
        private void buttonOyun5_Click(object sender, EventArgs e) { }
        private void buttonOyun6_Click(object sender, EventArgs e) { }
        private void buttonOyun7_Click(object sender, EventArgs e) { }
        private void buttonOyun8_Click(object sender, EventArgs e) { }
        private void buttonOyun9_Click(object sender, EventArgs e) { }
    }
}