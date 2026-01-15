using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace oyunp
{
    public partial class oyunsec : Form
    {
        // DEĞİŞTİ: Yerel WMP kaldırıldı, MuzikYoneticisi kullanılıyor.
        private SoundPlayer efektOynatici;
        private Image orjinalSolResim, orjinalSagResim, orjinalAyarlarResmi;

        public oyunsec()
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

            try { efektOynatici = new SoundPlayer(Properties.Resources.tiksesi); } catch { }

            // DEĞİŞTİ: Müzik yükleme kodu MuzikYoneticisi'ne taşındı.

            if (pictureBoxSol != null) orjinalSolResim = pictureBoxSol.Image;
            if (pictureBoxSag != null) orjinalSagResim = pictureBoxSag.Image;
            if (pictureBoxAyarlar != null) orjinalAyarlarResmi = pictureBoxAyarlar.Image;

            this.Resize += new EventHandler(oyunsec_Resize);
        }

        private void oyunsec_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            // DEĞİŞTİ: Merkezi müzik başlatılıyor.
            MuzikYoneticisi.Baslat();

            YerlesimiAyarla();
        }

        private void oyunsec_Resize(object sender, EventArgs e)
        {
            YerlesimiAyarla();
        }

        private void YerlesimiAyarla()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            if (pictureBoxAyarlar != null)
            {
                int boyut = Math.Max(60, (int)(w * 0.08));
                pictureBoxAyarlar.Size = new Size(boyut, boyut);
                pictureBoxAyarlar.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxAyarlar.Location = new Point((w - boyut) / 2, h - boyut - 40);
                YuvarlakYap(pictureBoxAyarlar);
            }

            if (pictureBoxSol != null)
            {
                int boyut = (int)(w * 0.30);
                pictureBoxSol.Size = new Size(boyut, boyut);
                pictureBoxSol.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSol.Location = new Point((w / 4) - (boyut / 2), (h / 2) - (boyut / 2));
            }

            if (pictureBoxSag != null)
            {
                int boyut = (int)(w * 0.30);
                pictureBoxSag.Size = new Size(boyut, boyut);
                pictureBoxSag.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSag.Location = new Point((w * 3 / 4) - (boyut / 2), (h / 2) - (boyut / 2));
            }
        }

        private void YuvarlakYap(PictureBox kutu)
        {
            using (GraphicsPath yol = new GraphicsPath())
            {
                yol.AddEllipse(0, 0, kutu.Width, kutu.Height);
                kutu.Region = new Region(yol);
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

        private void EfektUygula(PictureBox kutu, Image orjinal, bool giris)
        {
            if (kutu == null || orjinal == null) return;
            this.SuspendLayout();
            if (giris)
            {
                kutu.Image = ResmiParlat(orjinal);
                kutu.Size = new Size(kutu.Width + 20, kutu.Height + 20);
                kutu.Location = new Point(kutu.Left - 10, kutu.Top - 10);
            }
            else
            {
                kutu.Image = orjinal;
                YerlesimiAyarla();
            }
            if (kutu == pictureBoxAyarlar) YuvarlakYap(kutu);
            this.ResumeLayout(true);
        }

        private void pictureBoxSol_MouseEnter(object sender, EventArgs e) => EfektUygula(pictureBoxSol, orjinalSolResim, true);
        private void pictureBoxSol_MouseLeave(object sender, EventArgs e) => EfektUygula(pictureBoxSol, orjinalSolResim, false);

        private void pictureBoxSag_MouseEnter(object sender, EventArgs e) => EfektUygula(pictureBoxSag, orjinalSagResim, true);
        private void pictureBoxSag_MouseLeave(object sender, EventArgs e) => EfektUygula(pictureBoxSag, orjinalSagResim, false);

        private void pictureBoxAyarlar_MouseEnter(object sender, EventArgs e) => EfektUygula(pictureBoxAyarlar, orjinalAyarlarResmi, true);
        private void pictureBoxAyarlar_MouseLeave(object sender, EventArgs e) => EfektUygula(pictureBoxAyarlar, orjinalAyarlarResmi, false);

        private void EfektSesiCal() { try { efektOynatici.Play(); } catch { } }

        private void pictureBoxSol_Click(object sender, EventArgs e)
        {
            EfektSesiCal();
            // DEĞİŞTİ: Müzik durdurma kodu kaldırıldı, böylece müzik devam eder.

            KullaniciForms kForm = new KullaniciForms();
            kForm.gelenOyunTuru = "sol";

            if (this.WindowState == FormWindowState.Maximized)
                kForm.WindowState = FormWindowState.Maximized;

            kForm.Show();
            this.Hide();
        }

        private void pictureBoxSag_Click(object sender, EventArgs e)
        {
            EfektSesiCal();
            // DEĞİŞTİ: Müzik durdurma kodu kaldırıldı.

            KullaniciForms kForm = new KullaniciForms();
            kForm.gelenOyunTuru = "sag";

            if (this.WindowState == FormWindowState.Maximized)
                kForm.WindowState = FormWindowState.Maximized;

            kForm.Show();
            this.Hide();
        }

        private void pictureBoxAyarlar_Click(object sender, EventArgs e)
        {
            EfektSesiCal();
            // DEĞİŞTİ: Ses seviyesi yöneticiden alınıyor.
            int suankiSes = MuzikYoneticisi.SesSeviyesiniGetir();

            AyarlarForm ayarlar = new AyarlarForm(suankiSes, (yeniSes) =>
            {
                MuzikYoneticisi.SesAyarla(yeniSes);
            });
            ayarlar.ShowDialog();
        }

        private void oyunsec_FormClosed(object sender, FormClosedEventArgs e) { Application.Exit(); }
    }
}