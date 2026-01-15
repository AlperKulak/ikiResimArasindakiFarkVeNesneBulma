using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace oyunp
{
    public partial class Kayip : Form
    {
        // --- DEĞİŞKENLER ---
        private SoundPlayer tikSesi;

        // Hangi oyunun yeniden başlayacağını bilmek için
        private string oyunTuru; // "fark" veya "gizem"
        private string oyuncuIsmi;

        // --- EKRAN VE BUTON AYARLARI ---
        private float refGenislik = 1920f;
        private float refYukseklik = 1080f;

        // Buton Boyutları
        private int butonGenislik = 455;
        private int butonYukseklik = 200;

        // Parametreler: Oyun Türü ve Oyuncu İsmi
        public Kayip(string gelenOyunTuru, string gelenIsim)
        {
            InitializeComponent();

            oyunTuru = gelenOyunTuru;
            oyuncuIsmi = gelenIsim;

            // --- TİTREME ENGELLEYİCİ ---
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            // Arka planın düzgün uzaması için
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // SES
            try { tikSesi = new SoundPlayer(Properties.Resources.tiksesi); } catch { }

            // LABEL AYARLARI (lblZamanNick kullanıyoruz)
            if (lblZamanNick != null)
            {
                lblZamanNick.BackColor = Color.Transparent;
                lblZamanNick.ForeColor = Color.WhiteSmoke;

                // "Cinzel" fontu
                lblZamanNick.Font = new Font("Cinzel", 36, FontStyle.Bold);

                // Kaybetme Mesajı
                lblZamanNick.Text = $"SÜRE DOLDU!\n{gelenIsim.ToUpper()}\nMAALESEF BAŞARAMADIN...";
                lblZamanNick.TextAlign = ContentAlignment.MiddleCenter;
            }

            this.Load += Kayip_Load;
            this.Resize += Kayip_Resize;
        }

        // Parametresiz yapıcı (Designer hatası vermemesi için)
        public Kayip() : this("fark", "Misafir") { }

        private void Kayip_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            ButonlariDuzenle();
            ElemanlariYerlestir();
        }

        private void Kayip_Resize(object sender, EventArgs e)
        {
            ElemanlariYerlestir();
        }

        // --- KOORDİNAT SİSTEMİ ---
        private void ElemanlariYerlestir()
        {
            if (this.ClientSize.Width == 0) return;

            // LABEL KONUMLANDIRMA
            if (lblZamanNick != null)
            {
                lblZamanNick.AutoSize = true;
                float oranY = (float)this.ClientSize.Height / refYukseklik;
                int yeniY = (int)(300 * oranY);

                lblZamanNick.Location = new Point(
                    (this.ClientSize.Width - lblZamanNick.Width) / 2,
                    yeniY);
            }

            // BUTON KONUMLANDIRMA
            ButonKonumla(buttonMenu, 500, 830);
            ButonKonumla(buttonYeniden, 970, 830);
        }

        private void ButonKonumla(Control btn, int refX, int refY)
        {
            if (btn == null) return;

            float oranX = (float)this.ClientSize.Width / refGenislik;
            float oranY = (float)this.ClientSize.Height / refYukseklik;

            int yeniW = (int)(butonGenislik * oranX);
            int yeniH = (int)(butonYukseklik * oranY);

            int yeniX = (int)(refX * oranX);
            int yeniY = (int)(refY * oranY);

            btn.Size = new Size(yeniW, yeniH);
            btn.Location = new Point(yeniX, yeniY);
        }

        private void ButonlariDuzenle()
        {
            ButonStili(buttonMenu);
            ButonStili(buttonYeniden);
        }

        private void ButonStili(Button btn)
        {
            if (btn != null)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.Transparent;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 0, 0, 0);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 255, 255, 255);
                btn.Cursor = Cursors.Hand;
            }
        }

        private void TikSesiCal() { try { tikSesi.Play(); } catch { } }

        // --- BUTON TIKLAMA OLAYLARI ---

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            oyunsec anaMenu = new oyunsec();
            anaMenu.Show();
            this.Close();
        }

        private void buttonYeniden_Click(object sender, EventArgs e)
        {
            TikSesiCal();

            // Hangi oyundan geldiysek onu yeniden başlatıyoruz
            if (oyunTuru == "fark")
            {
                FormBul1 farkOyunu = new FormBul1(oyuncuIsmi);
                farkOyunu.WindowState = FormWindowState.Maximized;
                farkOyunu.Show();
            }
            else if (oyunTuru == "gizem")
            {
                GizemliNesne1 gizemOyunu = new GizemliNesne1(oyuncuIsmi);
                gizemOyunu.WindowState = FormWindowState.Maximized;
                gizemOyunu.Show();
            }

            this.Close();
        }
    }
}