using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using WMPLib; // Eklendi
using System.IO; // Eklendi

namespace oyunp
{
    public partial class GizemliNesne1 : Form
    {
        private string oyuncuIsmi;
        private int toplamNesne = 10;
        private int bulunanNesne = 0;
        private int toplamSure = 60;
        private int kalanSure = 60;
        private Timer zamanlayici;
        private float refGenislik = 1920f;
        private float refYukseklik = 1080f;
        private ProgressBar prgSure;
        private Label lblOyuncuAdi;

        private SoundPlayer dogruSesi;
        private SoundPlayer bitisSesi;
        private SoundPlayer kayipSesi;

        // YENİ: Saat sesi için WMP kullanıyoruz (Çakışmayı önler)
        private WindowsMediaPlayer saatSesiOynatici = new WindowsMediaPlayer();

        public GizemliNesne1(string gelenIsim)
        {
            InitializeComponent();
            oyuncuIsmi = string.IsNullOrEmpty(gelenIsim) ? "Misafir" : gelenIsim;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            try { dogruSesi = new SoundPlayer(Properties.Resources.dogru); } catch { }
            try { bitisSesi = new SoundPlayer(Properties.Resources.bitis); } catch { }
            try { kayipSesi = new SoundPlayer(Properties.Resources.kayip); } catch { }

            // Saat sesini WMP ile hazırla
            try
            {
                string yol = KaynagiTempDosyasinaCikar(Properties.Resources.saatsesi, "gecici_saatsesi.wav");
                saatSesiOynatici.URL = yol;
                saatSesiOynatici.settings.setMode("loop", true); // Sürekli çal
                saatSesiOynatici.settings.volume = 50;
            }
            catch { }

            zamanlayici = new Timer();
            zamanlayici.Interval = 1000;
            zamanlayici.Tick += Zamanlayici_Tick;

            this.Load += GizemliNesne1_Load;
            this.Resize += GizemliNesne1_Resize;
        }

        public GizemliNesne1() : this("Misafir") { }

        private string KaynagiTempDosyasinaCikar(Stream resource, string dosyaAdi)
        {
            string path = Path.Combine(Path.GetTempPath(), dosyaAdi);
            if (!File.Exists(path))
            {
                using (var fileStream = File.Create(path))
                {
                    resource.CopyTo(fileStream);
                }
            }
            return path;
        }

        private void GizemliNesne1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.LightSlateGray;

            ArayuzOlustur();
            ResimleriSolaDiz();
            NesneleriYerlestir();

            // Saat sesini başlat (WMP ile)
            try { saatSesiOynatici.controls.play(); } catch { }

            zamanlayici.Start();
        }

        private void GizemliNesne1_Resize(object sender, EventArgs e)
        {
            ResimleriSolaDiz();
            NesneleriYerlestir();
        }

        private void ArayuzOlustur()
        {
            lblOyuncuAdi = new Label();
            lblOyuncuAdi.Text = $"{oyuncuIsmi}";
            lblOyuncuAdi.Font = new Font("Arial", 16, FontStyle.Bold);
            lblOyuncuAdi.ForeColor = Color.White;
            lblOyuncuAdi.BackColor = Color.Transparent;
            lblOyuncuAdi.AutoSize = true;
            lblOyuncuAdi.Location = new Point(10, 10);
            this.Controls.Add(lblOyuncuAdi);

            prgSure = new ProgressBar();
            prgSure.Minimum = 0;
            prgSure.Maximum = toplamSure;
            prgSure.Value = kalanSure;
            prgSure.Step = 1;
            prgSure.Size = new Size(600, 25);
            prgSure.Location = new Point((this.ClientSize.Width - prgSure.Width) / 2, 10);
            this.Controls.Add(prgSure);
        }

        private void Zamanlayici_Tick(object sender, EventArgs e)
        {
            kalanSure--;
            if (prgSure != null && kalanSure >= 0) prgSure.Value = kalanSure;
            lblOyuncuAdi.Text = $"{oyuncuIsmi}\n{kalanSure} sn";

            if (kalanSure <= 0)
            {
                zamanlayici.Stop();
                try { saatSesiOynatici.controls.stop(); } catch { } // Sesi durdur
                try { kayipSesi.Play(); } catch { }

                Kayip kayipEkrani = new Kayip("gizem", oyuncuIsmi);
                kayipEkrani.Show();
                this.Close();
            }
        }

        private void NesneBulundu(Button btn, PictureBox ilgiliResim)
        {
            if (btn == null) return;

            // SoundPlayer ile çaldığı için WMP (saat sesi) kesilmez!
            try { dogruSesi.Play(); } catch { }

            btn.Visible = false;
            if (ilgiliResim != null) ilgiliResim.Visible = false;

            bulunanNesne++;

            if (bulunanNesne >= toplamNesne)
            {
                zamanlayici.Stop();
                try { saatSesiOynatici.controls.stop(); } catch { } // Sesi durdur
                try { bitisSesi.Play(); } catch { }

                Kazanma zaferEkrani = new Kazanma(oyuncuIsmi, kalanSure);
                zaferEkrani.Show();
                this.Close();
            }
        }

        private void ResimleriSolaDiz()
        {
            PictureBox[] resimler = { pbElma, pbKalem, pbCanta, pbKedi, pbDiploma, pbTelefon, pbTuy, pbBelge, pbGozluk, pbAnahtar };
            if (this.ClientSize.Height == 0) return;
            int resimBoyutu = 90;
            int aralik = 15;
            int baslangicX = 20;
            int baslangicY = 80;
            int gerekenYukseklik = (resimler.Length * (resimBoyutu + aralik)) + baslangicY;
            if (gerekenYukseklik > this.ClientSize.Height)
            {
                float oran = (float)this.ClientSize.Height / gerekenYukseklik;
                resimBoyutu = (int)(resimBoyutu * oran * 0.95f);
                aralik = (int)(aralik * oran);
            }
            for (int i = 0; i < resimler.Length; i++)
            {
                if (resimler[i] != null)
                {
                    resimler[i].Size = new Size(resimBoyutu, resimBoyutu);
                    resimler[i].Location = new Point(baslangicX, baslangicY + (i * (resimBoyutu + aralik)));
                    resimler[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    resimler[i].BackColor = Color.FromArgb(100, 255, 255, 255);
                    resimler[i].BringToFront();
                }
            }
        }

        private void NesneleriYerlestir()
        {
            if (this.ClientSize.Width == 0) return;
            if (prgSure != null) prgSure.Location = new Point((this.ClientSize.Width - prgSure.Width) / 2, 10);
            ButonKonumla(buttonelma, 505, 825);
            ButonKonumla(buttonkalem, 1000, 950);
            ButonKonumla(buttoncanta, 1200, 550);
            ButonKonumla(buttonkedi, 1580, 480);
            ButonKonumla(buttondiploma, 1800, 700);
            ButonKonumla(buttontelefon, 1530, 825);
            ButonKonumla(buttontuy, 1300, 825);
            ButonKonumla(buttonbelge, 330, 700);
            ButonKonumla(buttongozluk, 250, 720);
            ButonKonumla(buttonanahtar, 340, 855);
        }

        private void ButonKonumla(Control btn, int x, int y)
        {
            if (btn == null) return;
            float oranX = (float)this.ClientSize.Width / refGenislik;
            float oranY = (float)this.ClientSize.Height / refYukseklik;
            int yeniX = (int)(x * oranX);
            int yeniY = (int)(y * oranY);
            int standartBoyut = 80;
            int yeniBoyut = (int)(standartBoyut * oranX);
            btn.Size = new Size(yeniBoyut, yeniBoyut);
            btn.Location = new Point(yeniX, yeniY);
            if (btn is Button b)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.BackColor = Color.Transparent;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                b.Cursor = Cursors.Default;
                b.Text = "";
            }
            btn.BringToFront();
        }

        private void buttonelma_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbElma);
        private void buttonkalem_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbKalem);
        private void buttoncanta_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbCanta);
        private void buttonkedi_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbKedi);
        private void buttondiploma_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbDiploma);
        private void buttontelefon_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbTelefon);
        private void buttontuy_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbTuy);
        private void buttonbelge_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbBelge);
        private void buttongozluk_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbGozluk);
        private void buttonanahtar_Click(object sender, EventArgs e) => NesneBulundu((Button)sender, pbAnahtar);

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ESC tuşuna basıldığında DURAKLATMA mantığı
            if (keyData == Keys.Escape)
            {
                // 1. OYUNU DURDUR (Zamanlayıcıyı durduruyoruz)
                zamanlayici.Stop();

                // Saat sesini duraklat
                try { saatSesiOynatici.controls.pause(); } catch { }

                // 2. AYARLAR FORMUNU AÇ
                int suankiSes = MuzikYoneticisi.SesSeviyesiniGetir();

                AyarlarForm ayarlar = new AyarlarForm(suankiSes, (yeniSes) =>
                {
                    // Ses ayarı değiştirilirse anlık olarak uygula
                    MuzikYoneticisi.SesAyarla(yeniSes);
                    try { saatSesiOynatici.settings.volume = yeniSes; } catch { }
                });

                // Kullanıcı Ayarlar formunu kapatana kadar oyun burada bekler
                ayarlar.ShowDialog();

                // 3. OYUNU DEVAM ETTİR
                // Ayarlar formu kapandı (Geri Dön tuşuna basıldı), süre işlemeye devam etsin
                zamanlayici.Start();
                try { saatSesiOynatici.controls.play(); } catch { }

                return true; // Tuş olayının işlendiğini belirtir
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}