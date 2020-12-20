using System;
using System.Collections.Generic;
using System.IO;
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
using Dashboard.AdminWindow.Add;
using Dashboard.AdminWindow.ViewModel;
using Dashboard.Common;
using Dashboard.Common.ViewModel;
using Dashboard.Data.Entities;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Dashboard.AdminWindow
{
    /// <summary>
    /// Interaction logic for ProductController.xaml
    /// </summary>
    public partial class ProductController : UserControl
    {
        public ProductController()
        {
            InitializeComponent();
            LoadDataProduct();
        }

        private string _pathImage = null;

        private void LoadDataProduct()
        {
            RenderDataProduct.Children.Clear();
            Db.Context.Imports.Load();
            var imports = Db.Context.Imports.Select(x => x).ToList();
            imports.Reverse();
            foreach (var import in imports)
            {
                //1. Select join
                var query = from p in Db.Context.Products
                            join pt in Db.Context.ProductInImports on p.ID equals pt.ProductID
                            where pt.ImportID == import.ID
                            select p;

                var expander = new DataListProduct();
                expander.epd_Header.Header = $"Phiếu nhập {import.Warehouse} từ {import.Supplier} - {import.DayImport}";
                expander.tbx_StockImport.Text = $"{import.Stock}";
                expander.tbx_DateImport.Text = $"{import.DayImport}";
                expander.tbx_Supplier.Text = $"{import.Supplier}";
                expander.tbx_Warehouse.Text = $"{import.Warehouse}";
                expander.tbx_DescriptionImport.Text = $"{import.Description}";

                //TODO: event cho Expander (phiues nhập hàng)
                expander.btn_Add.Click += (sender, args) =>
                {
                    var ap = new AddProduct(Status.AddProductImportOld, import.ID)
                    {
                        tbx_Supplier = {Text = import.Supplier},
                        tbx_DateImport = {Text = $"{import.DayImport.Date}"},
                        tbx_StockImport = {Text = $"{import.Stock}"},
                        tbx_Warehouse = {Text = import.Warehouse},
                        securityCodeImportOld = import.SecurityCode
                    };
                    
                    var window = new PageAddProductInImport(ap) {Title = "Thêm"};

                    window.ShowDialog();
                    LoadDataProduct();
                };
                expander.btn_Edit.Click += (sender, args) =>
                {
                    var editDL = new PageEditProduct();
                    if(expander.dtg_Product.SelectedIndex < 0) return;
                    var prod = (Product)expander.dtg_Product.SelectedValue;

                    var guarantee = Db.Context.ProductGuarantees.FirstOrDefault(x => x.ProductId == prod.ID);

                    //var category = from pic in Db.Context.ProductInCategories
                    //    join c in Db.Context.Categories on pic.CategoryID equals c.ID
                    //    select new{pic, c};
                    //var nameCategory = category.FirstOrDefault(x => x.pic.CategoryID == x.c.ID);
                    var ap = new AddProduct(Status.Edit, prod.ID)
                    {
                        tbx_Name = {Text = prod.Name},
                        tbx_Price = {Text = $"{prod.Price}"},
                        tbx_OriginalPrice = {Text = $"{prod.OriginalPrice}"},
                        tbx_Stock = {Text = $"{prod.Stock}"},
                        dp_WarrantyPeriod = {Text = $"{guarantee.ExpirationDate.Date}"},
                        tbx_SeriNumber = {Text = prod.SeriNumber},
                        card_Import = {Visibility = Visibility.Hidden}
                        //cbb_Categories = {Text = nameCategory != null ? $"{nameCategory.c.Name}" : ""}
                    };

                    //TODO: Event
                    ap.btn_AddProduct.Click += (o, eventArgs) => { editDL.Close(); LoadDataProduct(); };
                    ap.btn_CLose.Click += (o, eventArgs) =>  editDL.Close();
                    //ap.tbl_TitleProduct.MouseLeftButtonDown += (o, eventArgs) =>  editDL.DragMove();

                    editDL.RenderPages.Children.Add(ap);
                    editDL.ShowDialog();
                };
                expander.btn_Remove.Click += (sender, args) =>
                {
                    if (expander.dtg_Product.SelectedIndex < 0) return;

                    var sele = (Product)expander.dtg_Product.SelectedValue;
                    var prod = Db.Context.Products.Find(sele.ID);
                    if (prod == null) return;

                    var mess = new MessageDialog() { tbl_Title = { Text = "Xóa sản phẩm" }, tbl_Message = { Text = $"bạn thật sự muốn xóa {prod.Name}" } };
                    mess.ShowDialog();
                    if (mess.DialogResult != MyDialogResult.Result.Ok) return;
                    var pathImage = $@"{Directory.GetCurrentDirectory()}\{prod.ImagePath}";
                    if (File.Exists(pathImage))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        try
                        {
                            File.Delete(pathImage);

                        }
                        catch (Exception e)
                        {
                            var messErrorRemove = new MessageDialog(){tbl_Title = {Text = "Lỗi xóa hình"}, tbl_Message = {Text = $"Gặp lỗi xóa hình ảnh, bạn hãy tử xóa thủ công theo được dẫn: {Environment.NewLine} {pathImage}"} };
                            messErrorRemove.ShowDialog();
                        }
                    }
                    Db.Context.Products.Remove(prod);
                    var imp = Db.Context.Imports.Find(import.ID);
                    imp.Stock--;
                    if (imp.Stock < 1)
                        Db.Context.Imports.Remove(imp);
                    Db.Context.SaveChanges();
                    LoadDataProduct();
                };
                expander.dtg_Product.ItemsSource = query.ToList();
                
                RenderDataProduct.Children.Add(expander);

            }
        }


        private string SaveFile(string file)
        {
            var originalFileName = file;
            var fileName = $@"imageProduct/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            if (!Directory.Exists("imageProduct"))
                Directory.CreateDirectory("imageProduct");
            File.Copy(_pathImage, fileName);
            return fileName;
        }
        //private Result<string> CreateProduct()
        //{
        //    var product = new Product()
        //    {
        //        //Name = tbx_Name.Text,
        //        Price = Convert.ToDecimal(tbx_Price.Text),
        //        OriginalPrice = Convert.ToDecimal(tbx_OriginalPrice.Text),
        //        Stock = Convert.ToInt32(tbx_Stock.Text),
        //        DateCreated = DateTime.Now,
        //        ProductTranslations = new List<ProductTranslation>()
        //        {
        //            new ProductTranslation()
        //            {
        //                Name = tbx_Name.Text,
        //                Details = tbx_Details.Text
        //            }
        //        }
        //    };
        //    //Save Image
        //    if (_pathImage != null)
        //    {
        //        product.ProductImages = new List<ProductImage>()
        //        {
        //            new ProductImage()
        //            {
        //                Caption = "Thumbnail image",
        //                DateCreated = DateTime.Now,
        //                FileSize = new FileInfo(_pathImage).Length,
        //                ImagePath = this.SaveFile(_pathImage),
        //                IsDefault = true,
        //                SortOrder = 1
        //            }
        //        };
        //    }

        //    Db.Context.Products.Add(product);
        //    Db.Context.SaveChanges();
        //    return new ResultSuccess<string>();
        //}

        private DataProduct _dataProduct;
        private Result<string> EditOpenProduct()
        {
            //Fill data
            if (_dataProduct.dtg_Product.SelectedIndex < 0) return new ResultError<string>();
            var item = (Product)_dataProduct.dtg_Product.SelectedValue;
            var warranty = Db.Context.ProductGuarantees.FirstOrDefault(x => x.ProductId == item.ID);
            tbx_ID.Text = $"{item.ID}";
            tbx_Name.Text = item.Name;
            tbx_Price.Text = $"{item.Price}";
            tbx_OriginalPrice.Text = $"{item.OriginalPrice}";
            tbx_Stock.Text = $"{item.Stock}";
            tbx_SeriNumber.Text = item.SeriNumber;
            tbx_Details.Text = item.SeriNumber;
            if (warranty != null) dp_WarrantyPeriod.Text = $"{warranty.ExpirationDate.Date}";
            return new ResultSuccess<string>();
        }

        private Result<string> RemoveProduct()
        {
            return new ResultSuccess<string>();

        }

        private Result<string> SaveEditOpen()
        {
            var Id = int.Parse(tbx_ID.Text);
            var prod = Db.Context.Products.Find(Id);
            var guarantee = Db.Context.ProductGuarantees.FirstOrDefault(x => x.ProductId == prod.ID);

            prod.Name = tbx_Name.Text;
            prod.Price = Convert.ToDecimal(tbx_Price.Text);
            prod.OriginalPrice = Convert.ToDecimal(tbx_OriginalPrice.Text);
            prod.Stock = Convert.ToInt32(tbx_Stock.Text);
            prod.SeriNumber = tbx_SeriNumber.Text;
            prod.ImagePath = _pathImage != null ? this.SaveFile(_pathImage) : prod.ImagePath;

            if (guarantee != null) guarantee.ExpirationDate = Convert.ToDateTime(dp_WarrantyPeriod.Text);
            Db.Context.SaveChanges();
            return new ResultSuccess<string>();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void Btn_RemoveOpen_OnClick(object sender, RoutedEventArgs e)
        {
            dlh_Loading.IsOpen = true;
        }

        private void Btn_EditOpen_OnClick(object sender, RoutedEventArgs e)
        {
            if(_dataProduct.dtg_Product.SelectedIndex < 0) return;
            EditOpenProduct();
            btn_Do.Content = "Edit";
            dlh_Loading.IsOpen = true;
        }

        private void btn_AddOpen_Click(object sender, RoutedEventArgs e)
        {
            dlh_Loading.IsOpen = true;
            var pap = new PageAddProduct();
            pap.ShowDialog();
            LoadDataProduct();
        }

        private void btn_Do_Click(object sender, RoutedEventArgs e)
        {
            SaveEditOpen();
            dlh_Loading.IsOpen = false;
            RenderDataProduct.Children.Clear();
            _dataProduct = new DataProduct();
            RenderDataProduct.Children.Add(_dataProduct);
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

        private void Btn_ChangeView_OnClick(object sender, RoutedEventArgs e)
        {

            if (pi_List.Kind == PackIconKind.ViewListOutline)
            {
                pi_List.Kind = PackIconKind.ViewList;
                RenderDataProduct.Children.Clear();
                 _dataProduct = new DataProduct();
                 RenderDataProduct.Children.Add(_dataProduct);
                btn_EditOpen.Visibility = Visibility.Visible;
            }
            else
            {
                btn_EditOpen.Visibility = Visibility.Hidden;
                pi_List.Kind = PackIconKind.ViewListOutline;
                LoadDataProduct();
            }
        }

        private void btn_OpenCalendar_Click(object sender, RoutedEventArgs e)
        {
            dp_WarrantyPeriod.IsDropDownOpen = true;
        }
    }
}
