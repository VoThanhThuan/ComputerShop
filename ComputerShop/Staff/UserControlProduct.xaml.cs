using System;
using System.Collections.Generic;
using System.Linq;
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
using ControlzEx.Standard;

namespace Dashboard.Staff
{
    /// <summary>
    /// Interaction logic for UserControlProduct.xaml
    /// </summary>
    public partial class UserControlProduct : UserControl
    {
        public UserControlProduct()
        {
            InitializeComponent();
            LoadPanel();
        }

        public class ItemInList
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Price { get; set; }
        }

        private void LoadPanel()
        {
            var products = Db.Context.Products.Select(p => p);
            foreach (var product in products)
            {
                var cardProduct = new UserControlProductChild()
                {
                    TblNameProduct = { Text = product.Name },
                    TblAmount = { Text = $"{product.Stock}" },
                    TblUnitPrice = { Text = $"{product.Price:N0} VNĐ" }
                };

                //Add Items ListView
                var textAmount = int.Parse(cardProduct.TblAmount.Text);

                cardProduct.BtnAddCart.Click += (sender, args) =>
                {
                    if (textAmount < 1) return;
                    cardProduct.TblAmount.Text = $"{--textAmount}";
                    SelectedProduct.Items.Add(new ItemInList { Id = $"{product.ID}", Name = $"{product.Name}", Price = $"{product.Price:N0} VND" });
                };

                //Delete items ListView
                BtnDelete.Click += (sender, args) =>
                {
                    if (SelectedProduct.SelectedIndex < 0) return;
                    if (textAmount >= product.Stock) return;
                    cardProduct.TblAmount.Text = $"{++textAmount}";
                    SelectedProduct.Items.RemoveAt(SelectedProduct.SelectedIndex);
                };

                Product.Children.Add(cardProduct);
            }


        }



        //Calculator
        /* [Obsolete]
         private void Btn_Calc(object sender, RoutedEventArgs e)
         {
             System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
             if (p == null) return;
             p.WaitForInputIdle();
             NativeMethods.SetParent(p.MainWindowHandle, this.Handle);
         }
        `
         public IntPtr Handle { get; set; }*/
    }

}
