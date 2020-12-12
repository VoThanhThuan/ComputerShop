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

namespace Dashboard.AdminWindow.Add
{
    /// <summary>
    /// Interaction logic for PageAddProduct.xaml
    /// </summary>
    public partial class PageAddProduct : Window
    {
        public PageAddProduct()
        {
            InitializeComponent();
            var aii = new AddInforImport();
            aii.tbl_Title.PreviewMouseDown += Window_MouseDown;
            aii.btn_Cancel.Click += (sender, args) =>
            {
                this.Close();
            };
            aii.btn_Accept.Click += (sender, args) =>
            {
                RenderPages.Children.Clear();
                var ap = new AddProduct
                {
                    tbx_Supplier = {Text = aii.tbx_Supplier.Text},
                    tbx_Warehouse = {Text = aii.tbx_Warehouse.Text},
                    tbx_DateImport = {Text = $"{DateTime.Now}"},
                    tbx_DescriptionImport = {Text = aii.tbx_Description.Text}
                };

                ap.tbl_TitleImport.PreviewMouseDown += Window_MouseDown;
                ap.tbl_TitleProduct.PreviewMouseDown += Window_MouseDown;

                RenderPages.Children.Add(ap);

            };
            RenderPages.Children.Add(aii);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
