using System;
using System.IO;
using WMPLib; // WMPLib referansının ekli olduğundan emin ol

namespace oyunp
{
    public static class MuzikYoneticisi
    {
        // Statik olduğu için uygulama boyunca kapanmaz
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string muzikYolu = "";

        // Müziği Başlat (Eğer zaten çalıyorsa baştan başlatmaz!)
        public static void Baslat()
        {
            // Müzik dosyasını temp klasörüne çıkar (Sadece bir kere)
            if (string.IsNullOrEmpty(muzikYolu))
            {
                muzikYolu = KaynagiTempDosyasinaCikar(Properties.Resources.arkaplan, "gecici_arkaplan.wav");
                player.URL = muzikYolu;
                player.settings.setMode("loop", true); // Döngüye al
                player.settings.volume = 50; // Varsayılan ses
            }

            // Eğer şu an çalmıyorsa başlat, çalıyorsa elleme (Böylece kesilmez)
            if (player.playState != WMPPlayState.wmppsPlaying)
            {
                player.controls.play();
            }
        }

        public static void Durdur()
        {
            try { player.controls.stop(); } catch { }
        }

        public static void SesAyarla(int seviye)
        {
            try { player.settings.volume = seviye; } catch { }
        }

        public static int SesSeviyesiniGetir()
        {
            try { return player.settings.volume; } catch { return 50; }
        }

        // Kaynak çıkarma metodu 
        private static string KaynagiTempDosyasinaCikar(System.IO.Stream resource, string dosyaAdi)
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
    }
}