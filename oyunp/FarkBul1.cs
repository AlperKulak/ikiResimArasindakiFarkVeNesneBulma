using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;
using WMPLib;
using System.IO;

namespace oyunp
{
    public partial class FormBul1 : Form
    {
        private Size ResimBoyutu = new Size(675, 612);
        private Point SolResimNoktasi = new Point(50, 135);
        private Point SagResimNoktasi = new Point(805, 135);
        private Size BarBoyutu = new Size(800, 30);
        private Point BarNoktasi = new Point(400, 50);

        private List<Point> FarkKonumlari = new List<Point>()
        {
            new Point(85, 505), new Point(55, 700), new Point(560, 200),
            new Point(85, 450), new Point(600, 450), new Point(520, 450),
            new Point(420, 470), new Point(450, 520), new Point(600, 490),
            new Point(560, 660)
        };

        private int toplamSure = 60;
        private int kalanSure = 60;
        private int toplamFarkSayisi = 10;
        private int bulunanFarkSayisi = 0;
        private string oyuncuIsmi;

        private SoundPlayer dogruSesi;
        private SoundPlayer bitis;
        private SoundPlayer kayip;

        private WindowsMediaPlayer saatSesiOynatici = new WindowsMediaPlayer();

        Timer oyunZamanlayici = new Timer();
        ProgressBar prgSure = new ProgressBar();

        public FormBul1(string gelenIsim)
        {
            InitializeComponent();
            oyuncuIsmi = string.IsNullOrEmpty(gelenIsim) ? "Misafir" : gelenIsim;

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            try { dogruSesi = new SoundPlayer(Properties.Resources.dogru); } catch { }
            try { bitis = new SoundPlayer(Properties.Resources.bitis); } catch { }
            try { kayip = new SoundPlayer(Properties.Resources.kayip); } catch { }

            try
            {
                string yol = KaynagiTempDosyasinaCikar(Properties.Resources.saatsesi, "gecici_saatsesi.wav");
                saatSesiOynatici.URL = yol;
                saatSesiOynatici.settings.setMode("loop", true);
                saatSesiOynatici.settings.volume = 50;
            }
            catch { }

            oyunZamanlayici.Interval = 1000;
            oyunZamanlayici.Tick += OyunZamanlayici_Tick;

            this.Load += FormBul1_Load;
            this.Resize += FormBul1_Resize;
        }

        public FormBul1() : this("Misafir") { }

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Enter veya Space tuşlarını engelleme (varsayılan davranış)
            if (keyData == Keys.Space || keyData == Keys.Enter) return true;

            // ESC tuşuna basıldığında DURAKLATMA mantığı
            if (keyData == Keys.Escape)
            {
                // 1. OYUNU DURDUR
                oyunZamanlayici.Stop();

                // Saat sesini duraklat (Varsa)
                try { saatSesiOynatici.controls.pause(); } catch { }

                // 2. AYARLAR FORMUNU AÇ (ShowDialog kodun burada beklemesini sağlar)
                // Mevcut ses seviyesini alarak gönderiyoruz
                int suankiSes = MuzikYoneticisi.SesSeviyesiniGetir();

                AyarlarForm ayarlar = new AyarlarForm(suankiSes, (yeniSes) =>
                {
                    // Hem genel müziğin hem de oyun içindeki saat sesinin seviyesini güncelle
                    MuzikYoneticisi.SesAyarla(yeniSes);
                    try { saatSesiOynatici.settings.volume = yeniSes; } catch { }
                });

                // Form açılır ve kullanıcı "Geri Dön" veya "Çıkış" diyene kadar burası bekler.
                ayarlar.ShowDialog();

                // 3. OYUNU DEVAM ETTİR (Ayarlar kapandıktan sonra burası çalışır)
                // Eğer kullanıcı çıkışa bastıysa zaten Application.Exit çalışmış olur, buraya gelmez.
                oyunZamanlayici.Start();
                try { saatSesiOynatici.controls.play(); } catch { }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FormBul1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            prgSure.Maximum = toplamSure;
            prgSure.Value = kalanSure;
            this.Controls.Add(prgSure);

            ElemanlariYerlestir();

            if (label1 != null)
            {
                // DEĞİŞİKLİK BURADA: Başlangıçta metni "İsim \n Süre" formatına getirdik
                label1.Text = $"{oyuncuIsmi}\n{kalanSure} sn";

                label1.Location = new Point(20, 20);
                label1.BackColor = Color.Transparent;
                label1.ForeColor = Color.White;
                // Yazı tipini biraz büyütüp kalınlaştırmak görünürlüğü artırır (İsteğe bağlı)
                label1.Font = new Font("Arial", 16, FontStyle.Bold);
                label1.AutoSize = true;
                label1.BringToFront();
            }

            SkorGuncelle();
            ButonlariHazirla();
            prgSure.BringToFront();

            if (lblBulunan != null)
            {
                lblBulunan.BringToFront();
                lblBulunan.BackColor = Color.Transparent;
                lblBulunan.ForeColor = Color.White;
                lblBulunan.Font = new Font("Arial", 24, FontStyle.Bold);
            }

            try { saatSesiOynatici.controls.play(); } catch { }

            kalanSure = toplamSure;
            oyunZamanlayici.Start();
        }

        private void FormBul1_Resize(object sender, EventArgs e)
        {
            ElemanlariYerlestir();
            SkorGuncelle();
        }

        private void ElemanlariYerlestir()
        {
            if (pictureBox1 != null) { pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; pictureBox1.Size = ResimBoyutu; pictureBox1.Location = SolResimNoktasi; }
            if (pictureBox2 != null) { pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; pictureBox2.Size = ResimBoyutu; pictureBox2.Location = SagResimNoktasi; }
            if (prgSure != null) { prgSure.Size = BarBoyutu; prgSure.Location = BarNoktasi; }
        }

        private void ButonlariHazirla()
        {
            for (int i = 1; i <= toplamFarkSayisi; i++)
            {
                string butonAdi = "fark" + i;
                Control btn = this.Controls[butonAdi];
                if (btn != null && btn is Button)
                {
                    Button b = (Button)btn;
                    Point formKonumu = (i - 1 < FarkKonumlari.Count) ? FarkKonumlari[i - 1] : b.Location;
                    b.FlatStyle = FlatStyle.Flat;
                    b.BackColor = Color.Transparent;
                    b.FlatAppearance.BorderSize = 0;
                    b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    b.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 255, 0, 0);
                    b.Text = "";
                    b.Cursor = Cursors.Default;
                    b.Size = new Size(40, 40);
                    b.TabStop = false;

                    if (pictureBox1 != null)
                    {
                        b.Parent = pictureBox1;
                        b.Location = new Point(formKonumu.X - pictureBox1.Location.X, formKonumu.Y - pictureBox1.Location.Y);
                    }
                    else { b.Location = formKonumu; }

                    b.BringToFront();
                    b.Click -= FarkButonu_Click;
                    b.Click += FarkButonu_Click;
                }
            }
        }

        private void FarkButonu_Click(object sender, EventArgs e)
        {
            Button tiklanan = (Button)sender;
            tiklanan.Enabled = false;
            tiklanan.BackColor = Color.FromArgb(150, 255, 0, 0);
            this.ActiveControl = null;

            try { dogruSesi.Play(); } catch { }

            bulunanFarkSayisi++;
            SkorGuncelle();

            if (bulunanFarkSayisi >= toplamFarkSayisi)
            {
                oyunZamanlayici.Stop();
                try { saatSesiOynatici.controls.stop(); } catch { }
                try { bitis.Play(); } catch { }
                Kazanma zaferEkrani = new Kazanma(oyuncuIsmi, kalanSure);
                zaferEkrani.Show();
                this.Close();
            }
        }

        private void OyunZamanlayici_Tick(object sender, EventArgs e)
        {
            kalanSure--;
            if (kalanSure >= 0) prgSure.Value = kalanSure;

            // DEĞİŞİKLİK BURADA: Her saniye label1 güncelleniyor
            if (label1 != null)
            {
                label1.Text = $"{oyuncuIsmi}\n{kalanSure} sn";
            }

            if (kalanSure <= 0)
            {
                oyunZamanlayici.Stop();
                try { saatSesiOynatici.controls.stop(); } catch { }
                try { kayip.Play(); } catch { }
                Kayip kayipEkrani = new Kayip("fark", oyuncuIsmi);
                kayipEkrani.Show();
                this.Close();
                return;
            }
        }

        private void SkorGuncelle()
        {
            if (lblBulunan != null)
            {
                lblBulunan.Text = $"{bulunanFarkSayisi}/{toplamFarkSayisi}";
                lblBulunan.AutoSize = true;
                int x = (this.ClientSize.Width - lblBulunan.Width) / 2;
                int y = this.ClientSize.Height - 100;
                lblBulunan.Location = new Point(x, y);
            }
        }
    }
}