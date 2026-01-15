using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace oyunp
{
    // ---------------------------------------------------------
    // 1. ADIM: Interface Tanımı
    // Bölüm seçme ekranlarının sahip olması gereken yetenekler
    // ---------------------------------------------------------
    public interface IOyunMenuKontrolu
    {
        void MuzigiBaslat();
        void BolumBaslat(int bolumNo);
        void AyarlariGoster();
    }

    // ---------------------------------------------------------
    // 2. ADIM: Sınıf Tanımı
    // Interface'i (: IOyunMenuKontrolu) ekledik
    // ---------------------------------------------------------
    public partial class FarkBulma : Form, IOyunMenuKontrolu
    {
        private float refGenislik = 1920f;
        private float refYukseklik = 1080f;
        private int orjBtnW = 280;
        private int orjBtnH = 210;
        private int ayarBtnW = 100;
        private int ayarBtnH = 100;
        private int kaydirmaX = 100;
        private int kaydirmaY = 25;
        private string oyuncuIsmi;
        SoundPlayer tikSesi;

        private List<Point> hedefNoktalar = new List<Point>()
        {
          new Point(482, 149), new Point(839, 157), new Point(1175, 157),
          new Point(482, 427), new Point(839, 427), new Point(1175, 427),
          new Point(482, 732), new Point(839, 732), new Point(1175, 732)
        };

        public FarkBulma(string gelenIsim)
        {
            InitializeComponent();
            oyuncuIsmi = string.IsNullOrEmpty(gelenIsim) ? "Misafir" : gelenIsim;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            try { tikSesi = new SoundPlayer(Properties.Resources.tiksesi); } catch { }

            // Interface metodu üzerinden müziği başlatıyoruz
            MuzigiBaslat();

            this.Load += FarkBulma_Load;
            this.Resize += FarkBulma_Resize;
        }

        public FarkBulma() : this("Misafir") { }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        // ---------------------------------------------------------
        // 3. ADIM: Interface Metotlarının Uygulanması
        // ---------------------------------------------------------

        // Arayüzden gelen: Müziği Başlatma Kuralı
        public void MuzigiBaslat()
        {
            MuzikYoneticisi.Baslat();
        }

        // Arayüzden gelen: Bölüm Açma Kuralı
        public void BolumBaslat(int bolumNo)
        {
            if (bolumNo == 1)
            {
                // Oyuna girerken menü müziğini durdur
                MuzikYoneticisi.Durdur();

                FormBul1 oyun1 = new FormBul1(oyuncuIsmi);
                if (this.WindowState == FormWindowState.Maximized)
                    oyun1.WindowState = FormWindowState.Maximized;

                oyun1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(bolumNo + ". Bölüm yakında!");
            }
        }

        // Arayüzden gelen: Ayarları Gösterme Kuralı
        public void AyarlariGoster()
        {
            int suankiSes = MuzikYoneticisi.SesSeviyesiniGetir();
            AyarlarForm ayarlar = new AyarlarForm(suankiSes, (yeniSes) =>
            {
                MuzikYoneticisi.SesAyarla(yeniSes);
            });
            ayarlar.ShowDialog();
        }
        // ---------------------------------------------------------

        private void FarkBulma_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            ElemanlariYerlestirVeBagla();
        }

        private void FarkBulma_Resize(object sender, EventArgs e) { ElemanlariYerlestirVeBagla(); }

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
                if (btnAyar is Button b)
                {
                    b.FlatStyle = FlatStyle.Flat; b.BackColor = Color.Transparent; b.FlatAppearance.BorderSize = 0;
                }
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

        // --- BUTON TIKLAMALARI ARTIK INTERFACE METOTLARINI ÇAĞIRIYOR ---

        private void GenelOyunButonu_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            Button tiklananButon = (Button)sender;
            int butonNo = Convert.ToInt32(tiklananButon.Tag);

            // Eski kod yerine Interface metodunu çağırıyoruz
            BolumBaslat(butonNo);
        }

        private void buttonAyarlar_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            // Eski kod yerine Interface metodunu çağırıyoruz
            AyarlariGoster();
        }

        private void TikSesiCal() { try { tikSesi.Play(); } catch { } }

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