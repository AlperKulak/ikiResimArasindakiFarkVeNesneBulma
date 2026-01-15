using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace oyunp
{
    public partial class Kazanma : Form
    {
        // --- DEĞİŞKENLER ---
        private SoundPlayer tikSesi;

        // --- EKRAN VE BUTON AYARLARI (BURADAN DEĞİŞTİREBİLİRSİN) ---
        private float refGenislik = 1920f;
        private float refYukseklik = 1080f;

        // Buton Boyutlarını Buradan Ayarlayabilirsin:
        private int butonGenislik = 455;
        private int butonYukseklik = 200;

        public Kazanma(string nick, int kalanSure)
        {
            InitializeComponent();

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

            // LABEL AYARLARI (Cinzel Fontu)
            if (lblZamanNick != null)
            {
                lblZamanNick.BackColor = Color.Transparent;
                lblZamanNick.ForeColor = Color.WhiteSmoke; // Açık renk yazı

                // "Cinzel" fontu (Yüklü değilse varsayılan çalışır)
                lblZamanNick.Font = new Font("Cinzel", 36, FontStyle.Bold);

                lblZamanNick.Text = $"TEBRİKLER {nick.ToUpper()}!\n\nSÜRE: {60 - kalanSure} SN\nPUAN: {kalanSure * 10}";
                lblZamanNick.TextAlign = ContentAlignment.MiddleCenter;
            }

            this.Load += Kazanma_Load;
            this.Resize += Kazanma_Resize;
        }

        public Kazanma() : this("Misafir", 0) { }

        private void Kazanma_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            ButonlariDuzenle();
            ElemanlariYerlestir();
        }

        private void Kazanma_Resize(object sender, EventArgs e)
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

                // Referans olarak Y=300 konumunda dursun
                int yeniY = (int)(300 * oranY);

                lblZamanNick.Location = new Point(
                    (this.ClientSize.Width - lblZamanNick.Width) / 2,
                    yeniY);
            }

            // BUTON KONUMLANDIRMA (Referans 1920x1080'e göre)
            // Sol Buton (Menü) -> X: 500, Y: 800
            // Sağ Buton (Devam) -> X: 1100, Y: 800

            ButonKonumla(buttonMenu, 500, 830);
            ButonKonumla(buttonDevamet, 970, 830);
        }

        private void ButonKonumla(Control btn, int refX, int refY)
        {
            if (btn == null) return;

            float oranX = (float)this.ClientSize.Width / refGenislik;
            float oranY = (float)this.ClientSize.Height / refYukseklik;

            // Tepedeki değişkenleri kullanarak boyut hesaplama
            int yeniW = (int)(butonGenislik * oranX);
            int yeniH = (int)(butonYukseklik * oranY);

            // Konum hesaplama
            int yeniX = (int)(refX * oranX);
            int yeniY = (int)(refY * oranY);

            btn.Size = new Size(yeniW, yeniH);
            btn.Location = new Point(yeniX, yeniY);
        }

        private void ButonlariDuzenle()
        {
            ButonStili(buttonMenu);
            ButonStili(buttonDevamet);
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

        private void buttonDevamet_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            MessageBox.Show("2. Bölüm yapım aşamasında. Takipte kalın!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}