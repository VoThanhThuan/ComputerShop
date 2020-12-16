using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Dashboard.AdminWindow.Add
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : UserControl
    {
        public AddProduct()
        {
            InitializeComponent();
            dp_WarrantyPeriod.Text = $"{DateTime.Today.AddYears(2)}";
        }

        private Status _status = Status.Add;
        private int _ID = 0;

        public AddProduct(Status status, int id)
        {
            InitializeComponent();
            dp_WarrantyPeriod.Text = $"{DateTime.Today.AddYears(2)}";
            switch (status)
            {
                case Status.Edit:
                    _status = Status.Edit;
                    _ID = id;
                    btn_AddProduct.Content = "Edit";
                    btn_SaveListProdut.Visibility = Visibility.Collapsed;
                    break;
                case Status.AddOnlyProduct:
                    _ID = id;
                    _status = Status.AddOnlyProduct;
                    break;
            }
        }

        private string _pathImage = null;
        private List<Product> _products = new List<Product>();

        private string SaveFile(string file)
        {
            var originalFileName = file;
            var fileName = $@"imageProduct/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            if (!Directory.Exists("imageProduct"))
                Directory.CreateDirectory("imageProduct");
            File.Copy(_pathImage, fileName);
            return fileName;
        }

        private Result<string> AddListProduct()
        {
            var codeGuarantee = cb_seri.IsChecked == true ? tbx_SeriNumber.Text : $"{Guid.NewGuid()}";
            var product = new Product()
            {
                Name = tbx_Name.Text,
                Price = Convert.ToDecimal(tbx_Price.Text),
                OriginalPrice = Convert.ToDecimal(tbx_Price.Text),
                Stock = Convert.ToInt32(tbx_Stock.Text),
                DateCreated = DateTime.Now,
                SeriNumber = codeGuarantee,
                ImagePath = _pathImage != null ? this.SaveFile(_pathImage) : "",
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = tbx_Name.Text,
                        Details = tbx_Details.Text,
                    }
                },
                ProductGuarantee = new ProductGuarantee()
                {
                    DateOfPurchase = DateTime.Now,
                    ExpirationDate = string.IsNullOrEmpty(dp_WarrantyPeriod.Text) == true ? DateTime.Today.AddYears(2) : Convert.ToDateTime(dp_WarrantyPeriod.Text),
                    SeriNumber = codeGuarantee,
                    Description = tbx_Details.Text
                },

            };
            _products.Add(product);
            tbx_StockImport.Text = $"{_products.Count}";
            btn_SaveListProdut.IsEnabled = true;
            return new ResultSuccess<string>();
        }

        private void ResetAddProdut()
        {
            _pathImage = null;
            tbx_Name.Clear();
            tbx_Price.Clear();
            tbx_OriginalPrice.Clear();
            tbx_SeriNumber.Clear();
            tbx_Details.Clear();
            tbx_Stock.Clear();
            img_Product.Source = null;
            tbx_Name.Focus();
        }

        private Result<string> SaveListProduct()
        {
            var SecuriryCodeImport = Guid.NewGuid();
            if (_status != Status.AddOnlyProduct)
            {
                var import = new Import
                {
                    DayImport = Convert.ToDateTime(tbx_DateImport.Text),
                    Supplier = tbx_Supplier.Text,
                    Warehouse = tbx_Warehouse.Text,
                    Stock = Convert.ToInt32(tbx_StockImport.Text),
                    Description = tbx_DescriptionImport.Text,
                    SecurityCode = SecuriryCodeImport
                };
                Db.Context.Imports.Add(import);
                Db.Context.SaveChanges();
            }

            foreach (var item in _products)
            {
                Db.Context.Products.Add(item);
            }
            Db.Context.SaveChanges();
            var importID = Db.Context.Imports.FirstOrDefault(x => x.SecurityCode == SecuriryCodeImport);
            if (importID == null)
            {
                MessageBox.Show("Lỗi truy tim mã nhập hàng");
                return new ResultError<string>("Lỗi truy tim mã nhập hàng");
            }
            foreach (var import in _products.Select(item => new ProductInImport()
            {
                ProductID = item.ID,
                ImportID = importID.ID
            }))
            {
                Db.Context.ProductInImports.Add(import);
            }
            Db.Context.SaveChanges();
            return new ResultSuccess<string>();
        }

        private Result<string> EditProdut()
        {

            var prod = Db.Context.Products.Find(_ID);
            if (prod == null) return new ResultError<string>("Id không tồn tại");

            var guarantee = Db.Context.ProductGuarantees.FirstOrDefault(x => x.ProductId == prod.ID);

            var codeGuarantee = cb_seri.IsChecked == true ? tbx_SeriNumber.Text : $"{Guid.NewGuid()}";
            prod.Name = tbx_Name.Text;
            prod.Price = Convert.ToDecimal(tbx_Price.Text);
            prod.OriginalPrice = Convert.ToDecimal(tbx_Price.Text);
            prod.Stock = Convert.ToInt32(tbx_Stock.Text);
            prod.SeriNumber = codeGuarantee;
            prod.ImagePath = _pathImage != null ? this.SaveFile(_pathImage) : prod.ImagePath;

            if (guarantee != null) guarantee.ExpirationDate = Convert.ToDateTime(dp_WarrantyPeriod.Text);

            Db.Context.SaveChanges();

            return new ResultSuccess<string>();
        }

        private static readonly Regex _regex = new Regex("[^0-9.,]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void GroupBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var opf = new OpenFileDialog();
            if (opf.ShowDialog() != true) return;
            _pathImage = opf.FileName;
            img_Product.Source = new BitmapImage(new Uri(_pathImage));
        }

        private void GroupBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            // Note that you can have more than one file.
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            _pathImage = files[0];
            img_Product.Source = new BitmapImage(new Uri(files[0]));
        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_status == Status.Add || _status == Status.AddOnlyProduct)
            {
                AddListProduct();
                ResetAddProdut();
            }
            else
            {
                EditProdut();
            }
        }

        private void btn_SaveListProdut_Click(object sender, RoutedEventArgs e)
        {
            SaveListProduct();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private void tbx_Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private void tbx_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = ((TextBox)sender);
            if (string.IsNullOrEmpty(txt.Text)) return;
            if (!IsTextAllowed(txt.Text))
            {
                var mess = new MessageDialog(){tbl_Title = {Text = "Không phải số"}, tbl_Message = {Text = "Dữ liệu bạn copy có chứ ký tự."}};
                mess.ShowDialog();
                txt.Clear();
                return;
            }
            txt.Text = $"{Convert.ToDouble(txt.Text):N0}";
            txt.CaretIndex = txt.Text.Length;

        }

        private void btn_OpenCalendar_Click(object sender, RoutedEventArgs e)
        {
            dp_WarrantyPeriod.IsDropDownOpen = true;
        }

        private void Tbx_Stock_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = ((TextBox)sender);
            if (IsTextAllowed(txt.Text)) return;
            var mess = new MessageDialog() { tbl_Title = { Text = "Không phải số" }, tbl_Message = { Text = "Dữ liệu bạn copy có chứ ký tự." } };
            mess.ShowDialog();
            txt.Clear();
            return;
        }

        private void Tbl_TitleImport_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.DragMove();
        }
    }

    public enum Status
    {
        Add = 0,
        AddOnlyProduct = 1,
        Edit = 2
    }
}
