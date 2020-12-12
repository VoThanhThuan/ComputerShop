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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.AdminWindow.ViewModel
{
    /// <summary>
    /// Interaction logic for DataProduct.xaml
    /// </summary>
    public partial class DataProduct : UserControl
    {
        public DataProduct()
        {
            InitializeComponent();
            Db.Context.Products.Load();
            var product = Db.Context.Products.Local;
            dtg_Product.ItemsSource = product.ToObservableCollection();
        }
    }
}
