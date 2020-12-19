using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
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


        private readonly List<UserControlProductChild> _listCard = new List<UserControlProductChild>();

        private void FindProduct()//TODO:Find
        {
            Product.Children.Clear();
            var products = _listCard.Where(x =>
                x.TblNameProduct.Text.ToLower()
                    .Contains(tbx_Find.Text.ToLower()));
            foreach (var product in products)
                Product.Children.Add(product);
        }
        //TODO:Find
        private void Tbx_Find_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            FindProduct();
        }

        //TODO:LoadPanel
        private void LoadPanel()
        {
            var products = Db.Context.Products.Select(p => p);

            //TODO: đây là danh sách lưu các textblock số lượng của cái card
            var listCart = new List<UCCartSelProduct>();

            foreach (var product in products)
            {
                var cardProduct = new UserControlProductChild()
                {
                    TblNameProduct = { Text = product.Name },
                    TblAmount = { Text = $"{product.Stock}" },
                    TblUnitPrice = { Text = $"{product.Price:N0}" },
                    TblId = { Text = $"{product.ID}" }
                };

                //TODO:Check hình ảnh còn trong máy hay không
                if (File.Exists($@"{Directory.GetCurrentDirectory()}\{product.ImagePath}"))
                    cardProduct.ImageProduct.Source =
                        new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\{product.ImagePath}"));
                _listCard.Add(cardProduct);

                var textAmount = int.Parse(cardProduct.TblAmount.Text);//TODO: Ép kiểu số lượng
                cardProduct.BtnAddCart.Click += (sender, args) =>//TODO: Add Cart
                {
                    var itemCart =
                        listCart.FirstOrDefault(x => (string)x.LabelCartId.Content == cardProduct.TblId.Text);
                    if (textAmount < 1) return;
                    cardProduct.TblAmount.Text = $"{--textAmount}";
                    if (itemCart == null)
                    {
                        var cartPro = new UCCartSelProduct()
                        {
                            ImageCartCh = cardProduct.ImageProduct,
                            TblName = { Text = product.Name },
                            TblAmount = { Text = $"1" },
                            TblPrice = { Text = $"{product.Price:N0}" },
                            LabelCartId = { Content = $"{product.ID}" }
                        };
                        listCart.Add(cartPro);

                        cartPro.chk_delete.Click += (o, eventArgs) =>
                        {
                            var cart = _listCard.FirstOrDefault(x =>
                                x.TblId.Text == (string)cartPro.LabelCartId.Content);
                            if (cart == null) return;
                            var l = int.Parse(cartPro.TblAmount.Text);
                            cartPro.TblAmount.Text = $"{--l}";
                            if (cartPro.TblAmount.Text == "0")
                            {
                                SelectedProduct.Children.Remove(cartPro);
                                listCart.Remove(cartPro);
                            }

                            var i = int.Parse(cart.TblAmount.Text);
                            cart.TblAmount.Text = $"{++i}";
                            ++textAmount;

                        };
                        SelectedProduct.Children.Add(cartPro);
                    }
                    else
                    {
                        var i = int.Parse(itemCart.TblAmount.Text);
                        itemCart.TblAmount.Text = $"{++i}";
                    }

                };//TODO: foreach Add Cart

                Product.Children.Add(cardProduct);
            }//TODO: foreach (var product in products)

            //TODO: TotalCost Fake
            BtnTotalCost.Click += (sender, args) =>
            {
                var sum = 0.0d;
                foreach (var item in listCart)
                {
                    var amount = int.Parse(item.TblAmount.Text);
                    var price = double.Parse(item.TblPrice.Text);
                    sum += amount * price;
                }

                LblTotalCost.Content = $"{sum:N0}";
            };

        }//TODO: LoadPenal();

        //TODO: Calculator
        public IntPtr Handle { get; set; }

        [Obsolete]
        private void BtnCalc_OnClick(object sender, RoutedEventArgs e)
        {
            var p = System.Diagnostics.Process.Start("calc.exe");
            if (p == null) return;
            p.WaitForInputIdle();
            _ = NativeMethods.SetParent(p.MainWindowHandle, this.Handle);
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            Product.Children.Clear();
            LoadPanel();
            SelectedProduct.Children.Clear();
            LblTotalCost.Content = "0";
        }

        //==================================================//
    }

    
}
