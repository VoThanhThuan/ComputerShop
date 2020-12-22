using Dashboard.Common;
using Dashboard.Common.ViewModel;
using Dashboard.Staff;
using DesignLogin;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dashboard
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnExit(object sender, RoutedEventArgs e)
        {
            MessExit();
        }

      

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            GridPrincipal.Children.Clear();
            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Add(new UserControlHome());
                    break;

                case 1:
                    GridPrincipal.Children.Add(new UserControlProduct());
                    break;

                case 2:
                    GridPrincipal.Children.Add(new PageUCSaleHistory());
                    break;
                case 3:
                    GridPrincipal.Children.Add(new UserControlGuarantee());
                    break;

            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 164 + (60 * index), 0, 0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnHide(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnZoom(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        } 
        
        private void MessExit()
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

        private void Mess_Logout()
        {
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = "Đăng Xuất" }
                ,
                tbl_Message = { Text = "Bạn có muốn đăng xuất ?" }
                ,
                btn_Cancel = { Content = "Ở lại" }
            };
            mess.ShowDialog();
            switch (mess.DialogResult)
            {
                case MyDialogResult.Result.Ok:
                    var login = new LoginWindow();
                    login.Show();
                    this.Close();
                    break;
                case MyDialogResult.Result.Cancel:
                    break;
            }
        }

        private void BtnLog_Out(object sender, RoutedEventArgs e)
        {
            Mess_Logout();
        }
    }
}
