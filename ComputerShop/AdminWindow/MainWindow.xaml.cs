using System;
using System.Collections.Generic;
using System.Linq;
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
using Dashboard.Common;
using Dashboard.Common.ViewModel;
using Dashboard.Data.Entities;
using DesignLogin;

namespace Dashboard.AdminWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new HomePage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = "Bạn Muốn Thoát ?" }
                ,
                tbl_Message = { Text = "Tắt chương trình hoặc trở lại màn hình đăng nhập" }
                ,
                btn_Cancel = { Content = "Login" }
            };
            mess.ShowDialog();
            switch (mess.DialogResult)
            {
                case MyDialogResult.Result.Ok:
                    Application.Current.Shutdown();
                    break;
                case MyDialogResult.Result.Cancel:
                    var login = new LoginWindow();
                    login.Show();
                    this.Close();
                    break;
            }
        }


        private void btn_User_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new UserController());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
                this.DragMove();
        }

        private void btn_Product_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new ProductController());
        }
    }
}
