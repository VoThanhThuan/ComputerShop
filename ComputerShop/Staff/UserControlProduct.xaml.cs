using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ControlzEx.Standard;
using Dashboard.Data.Entities;

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
            LoadCategories();
        }

        private List<UserControlProductChild> _listCard = new List<UserControlProductChild>();

        private List<Category> _category;
        private void LoadCategories()
        {
            _category = Db.Context.Categories.Select(x => x).ToList();
            cbbCategories.ItemsSource = _category;
            cbbCategories.DisplayMemberPath = "Name";
            cbbCategories.Text = "All";
        }

        private void FindProduct()//TODO:Find
        {
            Product.Children.Clear();
            var products = _listCard.Where(x =>
                x.TblNameProduct.Text.ToLower()
                    .Contains(tbx_Find.Text.ToLower()));
            foreach (var product in products)
                Product.Children.Add(product);
        }

        private void FindCategories()
        {
            if(cbbCategories.SelectedIndex < 0)return;
            var id = ((Category) cbbCategories.SelectedValue).ID;
            var pic = 
                Db.Context.ProductInCategories.Where(x => 
                    x.CategoryID == id).Select(x => x);
           
            Product.Children.Clear();
            foreach (var item in pic)
            {
                var products = 
                    _listCard.Where(x => 
                        x.TblId.Text == $"{item.ProductID}");
                foreach (var product in products)
                    Product.Children.Add(product);
            }


        }

        //TODO:Find
        private void Tbx_Find_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            FindProduct();
        }
        
        //TODO:LoadPanel
        private void LoadPanel()
        {
            Product.Children.Clear();
            SelectedProduct.Children.Clear();
            var products = Db.Context.Products.Select(p => p);

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

                //FIXME: Sửa cái này lại.
                //TODO:Check hình ảnh còn trong máy hay không
                if (File.Exists($@"{Directory.GetCurrentDirectory()}\{product.ImagePath}"))
                    cardProduct.ImageProduct.Source =
                        new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\{product.ImagePath}"));
                _listCard.Add(cardProduct);

                //TODO: Add UCCartSelProduct
                var textAmount = int.Parse(cardProduct.TblAmount.Text);//TODO: Ép kiểu số lượng
                cardProduct.BtnAddCart.Click += (sender, args) =>
                {
                    var itemCart =
                        listCart.FirstOrDefault(x => (string)x.LabelCartId.Content == cardProduct.TblId.Text);
                    if (textAmount < 1) return;
                    cardProduct.TblAmount.Text = $"{--textAmount}";
                  
                    if (itemCart == null)
                    {
                        var cartPro = new UCCartSelProduct()
                        {
                            TblName = { Text = product.Name },
                            TblAmount = { Text = $"1" },
                            TblPrice = { Text = $"{product.Price:N0}" },
                            LabelCartId = { Content = $"{product.ID}" }
                        };

                        //TODO:CHECK IMG
                        if (File.Exists($@"{Directory.GetCurrentDirectory()}\{product.ImagePath}"))
                            cartPro.ImageCartCh.Source =
                                new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\{product.ImagePath}"));

                        listCart.Add(cartPro);

                        //TODO: DELETE
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
                var sum = (from item in listCart 
                    let amount = int.Parse(item.TblAmount.Text) 
                    let price = double.Parse(item.TblPrice.Text) 
                    select amount * price).Sum();

                LblTotalCost.Content = $"{sum:N0}";
            };

            //BillPayment.Click += (sender, args) =>
            //{
            //    var bill = new Transaction()
            //    {
            //        TransactionDate = DateTime.Now,
            //        Amount = listCart.Count,
            //        Fee = decimal.Parse($"{LblTotalCost.Content}")
            //    };
            //};

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

        private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            _listCard = new List<UserControlProductChild>();
            LoadPanel();
            LblTotalCost.Content = "0";

        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindCategories();
        }




        //==================================================//
    }


}
