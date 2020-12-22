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
            LoadCategory();
        }

        private Status _status = Status.Add;
        private Guid? _ID = null;

        public AddProduct(Status status, Guid id)
        {
            InitializeComponent();
            dp_WarrantyPeriod.Text = $"{DateTime.Today.AddYears(2)}";
            LoadCategory();
            switch (status)
            {
                case Status.Edit:
                    _status = Status.Edit;
                    _ID = id;
                    var pic = Db.Context.ProductInCategories.FirstOrDefault(x => x.ProductID == id);
                    if (pic != null)
                    {
                        var category = Db.Context.Categories.Find(pic.CategoryID);
                        if (category != null)
                        {
                            cbb_Categories.SelectedValue = category;
                        }
                    }
                    btn_AddProduct.Content = "Edit";
                    btn_SaveListProdut.Visibility = Visibility.Collapsed;
                    break;
                case Status.AddProductImportOld:
                    _ID = id;
                    _status = Status.AddProductImportOld;
                    break;
            }

        }

        private string _pathImage = null;
        private List<Product> _products = new List<Product>();
        private List<ProductInCategory> _categories = new List<ProductInCategory>();
        private string SaveFile(string file)
        {
            var originalFileName = file;
            var fileName = $@"imageProduct\{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            if (!Directory.Exists("imageProduct"))
                Directory.CreateDirectory("imageProduct");
            File.Copy(_pathImage, fileName);
            return fileName;
        }

        private void LoadCategory()
        {
            var categories = Db.Context.Categories.Select(x => x);
            cbb_Categories.ItemsSource = categories.ToList();
            cbb_Categories.DisplayMemberPath = "Name";
        }

        private Result<string> AddListProduct()
        {

            if (string.IsNullOrEmpty(tbx_Name.Text))
            {
                var mess = new MessageDialog
                {
                    tbl_Title = {Text = "Lưu ý"}, tbl_Message = {Text = "Bạn đang để trống tên sản phẩm"}
                };
                mess.ShowDialog();
                if (mess.DialogResult != MyDialogResult.Result.Ok)
                    return new ResultError<string>("");
            }

            var codeGuarantee = cb_seri.IsChecked == true ? tbx_SeriNumber.Text : $"{Guid.NewGuid()}";
            var gid = Guid.NewGuid();
            var product = new Product()
            {
                ID = gid,
                Name = tbx_Name.Text,
                Price = string.IsNullOrEmpty(tbx_Price.Text) == true ? 0 : Convert.ToDecimal(tbx_Price.Text),
                OriginalPrice = string.IsNullOrEmpty(tbx_OriginalPrice.Text) == true ? 0 : Convert.ToDecimal(tbx_OriginalPrice.Text),
                Stock = string.IsNullOrEmpty(tbx_Stock.Text) == true ? 0 : Convert.ToInt32(tbx_Stock.Text),
                DateCreated = DateTime.Now,
                SeriNumber = codeGuarantee,
                ImagePath = _pathImage != null ? this.SaveFile(_pathImage) : "",
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

            //Add product in category
            if (cbb_Categories.SelectionBoxItem != null)
            {
                if (cbb_Categories.SelectedIndex > -1)
                {
                    var pic = new ProductInCategory();
                    pic.ProductID = gid;
                    pic.CategoryID = ((Category)cbb_Categories.SelectedValue).ID;
                    _categories.Add(pic);
                }
                else
                {
                    var mess = new MessageDialog {tbl_Title = {Text = "Lưu ý"}, tbl_Message = {Text = "Bạn đang để trống nhà cùng cấp"}};
                    mess.ShowDialog(); 
                }
            }

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


        public Guid securityCodeImportOld = Guid.NewGuid();
        private Result<string> SaveListProduct()
        {
            //FIXME:
            var securityCodeImport = _status == Status.AddProductImportOld ? securityCodeImportOld : Guid.NewGuid();
            var gid = Guid.NewGuid();
            if (_status != Status.AddProductImportOld)
            {
                var import = new Import
                {
                    ID = gid,
                    DayImport = Convert.ToDateTime(tbx_DateImport.Text),
                    Supplier = tbx_Supplier.Text,
                    Warehouse = tbx_Warehouse.Text,
                    Stock = Convert.ToInt32(tbx_StockImport.Text),
                    Description = tbx_DescriptionImport.Text,
                    SecurityCode = securityCodeImport

                };
                Db.Context.Imports.Add(import);
                Db.Context.SaveChanges();
            }
            else
            {
                var import = Db.Context.Imports.Find(_ID);
                if (import != null)
                    import.Stock += _products.Count();
            }
            //Product
            foreach (var item in _products)
            {
                Db.Context.Products.Add(item);
            }
            Db.Context.SaveChanges();

            //Category
            foreach (var item in _categories)
            {
                Db.Context.ProductInCategories.Add(item);
            }
            Db.Context.SaveChanges();



            //Add phiếu nhập hàng
            var importID = Db.Context.Imports.FirstOrDefault(x => x.SecurityCode == securityCodeImport);
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

            var pic = Db.Context.ProductInCategories.FirstOrDefault(x => x.ProductID == prod.ID);

            if (pic == null)
            {
                var addpic = new ProductInCategory();
                addpic.ProductID = prod.ID;
                addpic.CategoryID = ((Category)cbb_Categories.SelectedValue).ID;
                Db.Context.ProductInCategories.Add(addpic);
                Db.Context.SaveChanges();
            }
            else
            {
                pic.CategoryID = ((Category)cbb_Categories.SelectedValue).ID;
            }

            Db.Context.SaveChanges();
            return new ResultSuccess<string>();
        }

        #region Categories

        private Result<string> AddCategory(string name)
        {
            var valid = Db.Context.Categories.FirstOrDefault(x => x.Name == name);
            if (valid != null) return new ResultError<string>("Tên đã tồn tại");
            var category = new Category()
            {
                Name = name
            };
            Db.Context.Categories.Add(category);
            Db.Context.SaveChanges();
            LoadCategory();
            return new ResultSuccess<string>();
        }

        private Result<string> EditCategory(int id, string name)
        {
            var category = Db.Context.Categories.Find(id);
            if (category == null) return new ResultError<string>("Danh mục không tìm thấy");

            category.Name = name;

            Db.Context.SaveChanges();
            LoadCategory();

            return new ResultSuccess<string>();
        }

        private Result<string> RemoveCategory(int id)
        {
            var category = Db.Context.Categories.Find(id);
            if (category == null) return new ResultError<string>("Danh mục không tìm thấy");

            var pic = Db.Context.ProductInCategories.FirstOrDefault(x => x.CategoryID == category.ID);
            if (pic != null)
                Db.Context.ProductInCategories.Remove(pic);

            Db.Context.Remove(category);
            Db.Context.SaveChanges();
            LoadCategory();

            return new ResultSuccess<string>();

        }
        #endregion


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
            if (_status == Status.Add || _status == Status.AddProductImportOld)
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
                var mess = new MessageDialog() { tbl_Title = { Text = "Không phải số" }, tbl_Message = { Text = "Dữ liệu bạn copy có chứ ký tự." } };
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

        private void Btn_AddCategory_OnClick(object sender, RoutedEventArgs e)
        {

            var prompt = new PromptDialog() { tbl_Title = { Text = "Tạo \"Loại\" sản phẩm mới " }, };

            prompt.ShowDialog();
            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var result = AddCategory(prompt.tbx_Value.Text);

            if (result.IsSuccessed != false) return;
            var mess = new MessageDialog() { tbl_Title = { Text = "Lỗi thêm danh mục" }, tbl_Message = { Text = result.Message } };

            mess.ShowDialog();
        }

        private void Btn_EditCategory_OnClick(object sender, RoutedEventArgs e)
        {
            var prompt = new PromptDialog() { tbl_Title = { Text = "Chỉnh sửa \"Loại\" sản phẩm mới " }, tbx_Value = { Text = cbb_Categories.Text } };
            prompt.ShowDialog();
            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var result = EditCategory(((Category)cbb_Categories.SelectedValue).ID, prompt.tbx_Value.Text);

            if (result.IsSuccessed != false) return;
            var mess = new MessageDialog() { tbl_Title = { Text = "Lỗi Edit" }, tbl_Message = { Text = result.Message } };
            mess.ShowDialog();
        }

        private void Btn_RemoveCategory_OnClick(object sender, RoutedEventArgs e)
        {
            var category = ((Category)cbb_Categories.SelectedValue);
            var mess = new MessageDialog() { tbl_Title = { Text = "Xóa danh mục" }, tbl_Message = { Text = $"Bạn thật sự muốn xóa danh mục {category.Name}" } };
            mess.ShowDialog();
            if (mess.DialogResult != MyDialogResult.Result.Ok) return;

            var resultRemove = RemoveCategory(category.ID);
            if (resultRemove.IsSuccessed != false) return;
            mess = new MessageDialog() { tbl_Title = { Text = "Lỗi Xóa" }, tbl_Message = { Text = resultRemove.Message } };
            mess.ShowDialog();
        }
    }

    public enum Status
    {
        Add = 0,
        AddProductImportOld = 1,
        Edit = 2
    }
}
