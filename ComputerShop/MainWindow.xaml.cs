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
using Common;
using Dashboard.Staff;
using DesignLogin;
using MaterialDesignThemes.Wpf;

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
            var mess = new Message
            { lbl_Title = { Content = "Thoát" }, lbl_message = { Content = "Bạn thật sự muốn thoát" } };
            var result = mess.ShowDialog();
            if (result == true)
                Application.Current.Shutdown();

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            // Gọi thằng con 
            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlHome());
                    break;

                case 1:
                    GridPrincipal.Children.Clear();
                    /*  GridPrincipal.Children.Add(new UserControlHome);*/
                    break;

                case 2:
                    GridPrincipal.Children.Clear();
                   /* GridPrincipal.Children.Add(new UserControlInicio());*/
                    break;

                case 3:
                    GridPrincipal.Children.Clear();
                    break;

                case 4:
                    GridPrincipal.Children.Clear();
                    break;

                case 5:
                    GridPrincipal.Children.Clear();
                    break;

                default:
                    break;
            }
        }


        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
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
    }
}
