using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.Windows.Forms;

namespace oyunp
{
    // ---------------------------------------------------------
    // 1. ADIM: Interface Tanımı
    // Ayarlar formunun ne iş yapacağını belirleyen sözleşme
    // ---------------------------------------------------------
    public interface IAyarlarKontrolu
    {
        void SesSeviyesiniDegistir(int seviye);
        void FormuKapat();
        void OyundanCikisYap();
    }

    // ---------------------------------------------------------
    // 2. ADIM: Form Sınıfı
    // Interface'i (: IAyarlarKontrolu) ekleyerek imzalıyoruz
    // ---------------------------------------------------------
    public partial class AyarlarForm : Form, IAyarlarKontrolu
    {
        SoundPlayer tikSesiOynatici;
        private Action<int> SesDegistirmeFonksiyonu;
        private Image imgMuzik, imgGeri, imgCikis;

        public AyarlarForm(int baslangicSesi, Action<int> sesFonksiyonu)
        {
            InitializeComponent();
            SesDegistirmeFonksiyonu = sesFonksiyonu;

            // --- TİTREME ENGELLEYİCİ ---
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            // ---------------------------

            try { tikSesiOynatici = new SoundPlayer(Properties.Resources.tiksesi); } catch { }

            if (trackSes != null)
            {
                trackSes.Value = baslangicSesi;
                // Olay abonelikleri
                trackSes.Scroll -= TrackSes_Olayi;
                trackSes.ValueChanged -= TrackSes_Olayi;
                trackSes.Scroll += TrackSes_Olayi;
                trackSes.ValueChanged += TrackSes_Olayi;
            }

            if (lblSes != null)
            {
                lblSes.Text = baslangicSesi.ToString();
                lblSes.AutoSize = false;
                lblSes.TextAlign = ContentAlignment.MiddleCenter;
            }

            this.Resize += new EventHandler(AyarlarForm_Resize);
        }

        // --- EKRAN BOZULMASINI ÖNLEYEN SİHİRLİ KOD ---
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
        // 3. ADIM: Interface Metotlarının Uygulanması (Implementation)
        // ---------------------------------------------------------

        // Arayüzden gelen: Sesi değiştirme kuralı
        public void SesSeviyesiniDegistir(int seviye)
        {
            // Görsel güncelleme
            if (lblSes != null)
            {
                lblSes.Text = seviye.ToString();
                lblSes.Refresh();
            }

            // Ana fonksiyona haber verme (Action delegate)
            if (SesDegistirmeFonksiyonu != null)
                SesDegistirmeFonksiyonu(seviye);
        }

        // Arayüzden gelen: Formu kapatma kuralı
        public void FormuKapat()
        {
            TikSesiCal();
            this.Close();
        }

        // Arayüzden gelen: Çıkış yapma kuralı
        public void OyundanCikisYap()
        {
            TikSesiCal();
            if (MessageBox.Show("Çıkmak istiyor musun?", "Çıkış", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        // ---------------------------------------------------------

        private void AyarlarForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.SlateGray;

            HazirlikYap(btnMuzik, out imgMuzik);
            HazirlikYap(btnGeriDon, out imgGeri);
            HazirlikYap(btnOyundanCik, out imgCikis);

            YerlesimiAyarla();

            this.KeyPreview = true;
            // ESC tuşuna basılınca Interface metodunu çağırıyoruz
            this.KeyDown += (s, k) => { if (k.KeyCode == Keys.Escape) { FormuKapat(); } };
        }

        private void AyarlarForm_Resize(object sender, EventArgs e) { YerlesimiAyarla(); }

        private void YerlesimiAyarla()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            if (w < 100 || h < 100) return;

            this.SuspendLayout();
            int btnGenislik = (int)(w * 0.25);
            int btnYukseklik = (int)(h * 0.12);
            int ortakX = (w - btnGenislik) / 2;

            if (btnMuzik != null)
            {
                int muzikY = (int)(h * 0.15);
                btnMuzik.Size = new Size(btnGenislik, btnYukseklik);
                btnMuzik.SizeMode = PictureBoxSizeMode.StretchImage;
                btnMuzik.Location = new Point(ortakX, muzikY);
            }

            if (trackSes != null)
            {
                int trackY = (int)(h * 0.35);
                trackSes.Size = new Size(btnGenislik, 45);
                trackSes.Location = new Point(ortakX, trackY);
            }

            if (lblSes != null && trackSes != null)
            {
                float fontSize = Math.Max(16, h * 0.04f);
                lblSes.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
                lblSes.Size = new Size(btnGenislik, (int)(h * 0.06));
                int labelY = trackSes.Top - lblSes.Height - 5;
                lblSes.Location = new Point(ortakX, labelY);
            }

            if (btnGeriDon != null)
            {
                int geriY = (int)(h * 0.55);
                btnGeriDon.Size = new Size(btnGenislik, btnYukseklik);
                btnGeriDon.SizeMode = PictureBoxSizeMode.StretchImage;
                btnGeriDon.Location = new Point(ortakX, geriY);
            }

            if (btnOyundanCik != null)
            {
                int cikisY = (int)(h * 0.75);
                btnOyundanCik.Size = new Size(btnGenislik, btnYukseklik);
                btnOyundanCik.SizeMode = PictureBoxSizeMode.StretchImage;
                btnOyundanCik.Location = new Point(ortakX, cikisY);
            }
            this.ResumeLayout(true);
        }

        private void TrackSes_Olayi(object sender, EventArgs e)
        {
            if (trackSes == null) return;
            // Doğrudan kod yazmak yerine Interface metodunu kullanıyoruz
            SesSeviyesiniDegistir(trackSes.Value);
        }

        private void HazirlikYap(PictureBox btn, out Image img)
        {
            img = null;
            if (btn != null)
            {
                if (btn.Image == null && btn.BackgroundImage != null)
                {
                    btn.Image = btn.BackgroundImage;
                    btn.BackgroundImage = null;
                }
                btn.SizeMode = PictureBoxSizeMode.StretchImage;
                btn.BackColor = Color.Transparent;
                img = btn.Image;
            }
        }

        private void EfektYap(PictureBox btn, Image orjinalResim, bool giris)
        {
            if (btn == null || orjinalResim == null) return;
            Rectangle mevcutRec = new Rectangle(btn.Location, btn.Size);
            if (giris)
            {
                btn.Image = ResmiParlat(orjinalResim);
                int buyume = 20;
                btn.Size = new Size(mevcutRec.Width + buyume, mevcutRec.Height + buyume);
                btn.Location = new Point(mevcutRec.X - (buyume / 2), mevcutRec.Y - (buyume / 2));
            }
            else
            {
                btn.Image = orjinalResim;
                YerlesimiAyarla();
            }
        }

        private Bitmap ResmiParlat(Image resim)
        {
            if (resim == null) return null;
            Bitmap parlak = new Bitmap(resim.Width, resim.Height);
            using (Graphics g = Graphics.FromImage(parlak))
            {
                ColorMatrix matris = new ColorMatrix(new float[][] {
          new float[] {1.3f,0,0,0,0}, new float[] {0,1.3f,0,0,0},
          new float[] {0,0,1.3f,0,0}, new float[] {0,0,0,1,0}, new float[] {0,0,0,0,1}});
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorMatrix(matris);
                g.DrawImage(resim, new Rectangle(0, 0, resim.Width, resim.Height), 0, 0, resim.Width, resim.Height, GraphicsUnit.Pixel, attr);
            }
            return parlak;
        }

        private void TikSesiCal() { try { tikSesiOynatici.Play(); } catch { } }

        private void btnMuzik_MouseEnter(object sender, EventArgs e) => EfektYap(btnMuzik, imgMuzik, true);
        private void btnMuzik_MouseLeave(object sender, EventArgs e) => EfektYap(btnMuzik, imgMuzik, false);
        private void btnGeriDon_MouseEnter(object sender, EventArgs e) => EfektYap(btnGeriDon, imgGeri, true);
        private void btnGeriDon_MouseLeave(object sender, EventArgs e) => EfektYap(btnGeriDon, imgGeri, false);
        private void btnOyundanCik_MouseEnter(object sender, EventArgs e) => EfektYap(btnOyundanCik, imgCikis, true);
        private void btnOyundanCik_MouseLeave(object sender, EventArgs e) => EfektYap(btnOyundanCik, imgCikis, false);

        // --- BUTON TIKLAMALARI (Interface Metotlarını Çağırır) ---

        private void btnMuzik_Click(object sender, EventArgs e)
        {
            TikSesiCal();
            if (trackSes != null)
            {
                trackSes.Visible = !trackSes.Visible;
                if (lblSes != null) lblSes.Visible = trackSes.Visible;
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            // Doğrudan kod yazmak yerine Interface metodunu kullanıyoruz
            FormuKapat();
        }

        private void btnOyundanCik_Click(object sender, EventArgs e)
        {
            // Doğrudan kod yazmak yerine Interface metodunu kullanıyoruz
            OyundanCikisYap();
        }

        private void trackSes_Scroll(object sender, EventArgs e) { TrackSes_Olayi(sender, e); }
        private void trackSes_ValueChanged(object sender, EventArgs e) { TrackSes_Olayi(sender, e); }
    }
}