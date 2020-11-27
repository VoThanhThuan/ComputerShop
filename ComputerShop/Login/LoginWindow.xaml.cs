using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignLogin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            SetBackground();
        }


        private const UInt32 SPI_GETDESKWALLPAPER = 0x73;

        private const int MAX_PATH = 260;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(UInt32 uAction, int uParam, string lpvParam, int fuWinIni);
        public string GetCurrentDesktopWallpaper()
        {
            string currentWallpaper = new string('\0', MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, currentWallpaper.Length, currentWallpaper, 0);
            return currentWallpaper.Substring(0, currentWallpaper.IndexOf('\0'));
        }

        private void SetBackground()
        {
            //pictureBox1.Image = Image.FromFile(GetCurrentDesktopWallpaper());
            imgBackground.ImageSource = new BitmapImage(new Uri(GetCurrentDesktopWallpaper(), UriKind.Relative));
        }


    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var usuario = this.txt_Usuario.Text;
            var password = this.txt_Password.Password;

          
            switch (usuario)
            {
                case "" when password == "":
                {
                    FormError formError = new FormError();
                    formError.lbl_Usuario.Content = "Faltan ingresar datos";
                    formError.Show();
                    break;
                }
                case "fivecod" when password == "123":
                {
                    FormBienvenida formBienvenida = new FormBienvenida();
                    formBienvenida.lbl_Usuario.Content = usuario;
                    formBienvenida.Show();
                    break;
                }
                default:
                {
                    FormError formError = new FormError();
                    formError.lbl_Usuario.Content = usuario + " " + password;
                    formError.Show();
                    break;
                }
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            blurEffect.Radius = 3;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            blurEffect.Radius = 0;
        }
    }
    
}
