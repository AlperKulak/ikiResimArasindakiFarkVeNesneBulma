using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace oyunp
{
    // --- ASIL FORM SINIFI ---
    public partial class KullaniciForms : Form
    {
        // --- DEĞİŞKENLER ---
        private int txtNick_Genislik = 350;
        private int txtNick_Yukseklik = 50;
        private int btnGiris_Genislik = 350;
        private int btnGiris_Yukseklik = 100;
        private int btnGeri_Genislik = 350;
        private int btnGeri_Yukseklik = 100;
        private int txtNick_Y_Konumu = -180;
        private int btnGiris_Y_Konumu = -100;
        private int btnGeri_Y_Konumu = 0;

        public string gelenOyunTuru = "sol";
        SoundPlayer tikSesi;

        public KullaniciForms()
        {
            InitializeComponent();

            // --- TİTREME VE BEYAZ EKRAN ÇÖZÜMÜ ---
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            // -------------------------------------

            try { tikSesi = new SoundPlayer(Properties.Resources.tiksesi); } catch { }

            this.Load += KullaniciForms_Load;
            this.Resize += KullaniciForms_Resize;
        }

        private void KullaniciForms_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            KoordinatlariUygula();
        }

        private void KullaniciForms_Resize(object sender, EventArgs e) { KoordinatlariUygula(); }

        private void KoordinatlariUygula()
        {
            if (this.ClientSize.Width == 0) return;

            int merkezX = this.ClientSize.Width / 2;
            int merkezY = this.ClientSize.Height / 2;

            if (txtNick != null)
            {
                txtNick.AutoSize = false;
                txtNick.Size = new Size(txtNick_Genislik, txtNick_Yukseklik);
                txtNick.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                txtNick.Location = new Point(merkezX - (txtNick.Width / 2), merkezY + txtNick_Y_Konumu);
            }

            if (btnGiris != null)
            {
                btnGiris.Size = new Size(btnGiris_Genislik, btnGiris_Yukseklik);
                ButonStiliVer(btnGiris);
                btnGiris.Location = new Point(merkezX - (btnGiris.Width / 2), merkezY + btnGiris_Y_Konumu);
            }

            if (btnGeri != null)
            {
                btnGeri.Size = new Size(btnGeri_Genislik, btnGeri_Yukseklik);
                ButonStiliVer(btnGeri);
                btnGeri.Location = new Point(merkezX - (btnGeri.Width / 2), merkezY + btnGeri_Y_Konumu);
            }
        }

        // --- INTERFACE KULLANIMI ---
        private void btnGiris_Click(object sender, EventArgs e)
        {
            TikSesiCal();

            string girilenIsim = "";
            if (txtNick != null) girilenIsim = txtNick.Text.Trim();

            if (string.IsNullOrEmpty(girilenIsim))
            {
                MessageBox.Show("Lütfen bir isim giriniz!", "Hata");
                return;
            }

            // Interface referansı
            IOyunBaslatici oyunBaslatici = null;

            // STRATEJİ SEÇİMİ (Factory Logic)
            switch (gelenOyunTuru)
            {
                case "sol":
                    oyunBaslatici = new FarkBulmaBaslatici();
                    break;
                case "sag":
                    oyunBaslatici = new GizemliNesneBaslatici();
                    break;
                default:
                    MessageBox.Show("Bu oyun aktif değil.");
                    return;
            }

            // Seçilen stratejiyi uygula
            if (oyunBaslatici != null)
            {
                oyunBaslatici.OyunuBaslat(girilenIsim, this);
                // Not: Close işlemi Interface içindeki metoda taşınmaz çünkü "this" formu kapanırsa
                // kod akışı kesilebilir veya dispose olabilir. Genelde burada çağrılması daha güvenlidir.
                this.Close();
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            oyunsec secim = new oyunsec();
            secim.Show();
            this.Close();
        }

        private void TikSesiCal() { try { tikSesi.Play(); } catch { } }

        private void ButonStiliVer(Control btn)
        {
            if (btn is Button b)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.BackColor = Color.Transparent;
                b.FlatAppearance.BorderSize = 0;
                b.Cursor = Cursors.Hand;
            }
        }
    } // KullaniciForms BİTİŞİ

    // ---------------------------------------------------------
    // --- YENİ EKLENEN INTERFACE VE CLASS'LAR BURAYA GELİR ---
    // ---------------------------------------------------------

    // 1. Arayüz (Interface)
    public interface IOyunBaslatici
    {
        void OyunuBaslat(string oyuncuIsmi, Form mevcutForm);
    }

    // 2. Fark Bulma Oyunu Stratejisi
    public class FarkBulmaBaslatici : IOyunBaslatici
    {
        public void OyunuBaslat(string oyuncuIsmi, Form mevcutForm)
        {
            FarkBulma farkOyunu = new FarkBulma(oyuncuIsmi);
            if (mevcutForm.WindowState == FormWindowState.Maximized)
                farkOyunu.WindowState = FormWindowState.Maximized;

            farkOyunu.Show();
            // mevcutForm.Hide(); veya Close işlemini çağıran yer yapabilir.
        }
    }

    // 3. Gizemli Nesne Oyunu Stratejisi
    public class GizemliNesneBaslatici : IOyunBaslatici
    {
        public void OyunuBaslat(string oyuncuIsmi, Form mevcutForm)
        {
            GizemliNesne gizemliOyun = new GizemliNesne(oyuncuIsmi);
            if (mevcutForm.WindowState == FormWindowState.Maximized)
                gizemliOyun.WindowState = FormWindowState.Maximized;

            gizemliOyun.Show();
        }
    }

} // Namespace BİTİŞİ