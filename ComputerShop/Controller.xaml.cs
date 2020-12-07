using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dashboard.Data.EF;
using DesignLogin;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for Controller.xaml
    /// </summary>
    public partial class Controller : Window
    {
        public static CompuerShopDbContext _context;
        public Controller(CompuerShopDbContext context)
        {
            InitializeComponent();
            _context = context;
            //var login = new LoginWindow();
            // login.Show();
            StartDashboard();
        }

        public static void StartDashboard()
        {
            //var dashboard = new Social_Blade_Dashboard.MainWindow();
            //dashboard.Show();
            var mainProduct = new MainWindow();
            mainProduct.Show();
        }

    }
}
