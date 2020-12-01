using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Dashboard.Common;
using Dashboard.Data.EF;
using Dashboard.Data.Entities;
using Dashboard.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesignLogin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        /*
         private readonly DataContext dataContext;

        public MainWindow(DataContext dataContext)
        {
            InitializeComponent();

            this.dataContext = dataContext;
        }
 *
 */
        public LoginWindow()
        {
            InitializeComponent();
            SetBackground();
        }

        #region Methods

        private void AuthenticateUser()
        {
            var user = new UserService();

            var login = new LoginRequest()
            {
                Username = tbx_UserName.Text,
                Password = tbx_Password.Password,
                RememberMe = false
            };

            var result = user.Authenticate(login);
            if (result.IsSuccessed)
                this.Close();
            else
            {
                var formError = new FormError {lbl_Messege = {Content = $"{result.Message}"}, lbl_Username = {Content = tbx_UserName.Text}};
                formError.Show();
            }
        }

        #endregion

        /// <summary>
        /// Lấy ảnh nền desktop của người dùng
        /// </summary>
        private const uint SPI_GETDESKWALLPAPER = 0x73;

        private const int MAX_PATH = 260;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(uint uAction, int uParam, string lpvParam, int fuWinIni);
        public string GetCurrentDesktopWallpaper()
        {
            var currentWallpaper = new string('\0', MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, currentWallpaper.Length, currentWallpaper, 0);
            return currentWallpaper.Substring(0, currentWallpaper.IndexOf('\0'));
        }

        private void SetBackground()
        {
            var path = GetCurrentDesktopWallpaper();
            if(!File.Exists(path))
                return;
            imgBackground.ImageSource = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            AuthenticateUser();
        }

        private void btn_Close_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
    
}
