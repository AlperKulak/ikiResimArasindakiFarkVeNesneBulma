using System;
using System.Windows.Forms;

namespace oyunp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // PROJENİN BAŞLANGIÇ FORMU BURASI
            // Eğer senin formunun adı 'oyunsec' ise buna dokunma.
            // Eğer 'Form1' ise burayı 'new Form1()' yap.
            Application.Run(new Form1());
        }
    }
}