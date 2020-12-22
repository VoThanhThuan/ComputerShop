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
using Dashboard.Common.ViewModel;

namespace Dashboard.Staff
{
    /// <summary>
    /// Interaction logic for UserControlGuarantee.xaml
    /// </summary>
    public partial class UserControlGuarantee : UserControl
    {
        public UserControlGuarantee()
        {
            InitializeComponent();
        }
        
        private void Btn_Find_OnClick(object sender, RoutedEventArgs e)
        {
            var product = Db.Context.Products.FirstOrDefault(x => x.SeriNumber == tbx_Find.Text);

            if (product == null)
            {
                var mess = new MessageDialog(){tbl_Title = {Text = "Không tìm thấy" }, tbl_Message = {Text = $"không tìm thấy mã {tbx_Find.Text}"}};
                mess.ShowDialog();
            }
            else
            {
                var pig = Db.Context.ProductGuarantees.FirstOrDefault(x => x.ProductId == product.ID);
                if (pig == null)
                {
                    var messDontFind = new MessageDialog() { tbl_Title = { Text = "Bảo hành" }, tbl_Message = { Text = $"sản phẩm này không có bảo hành" } };
                    messDontFind.ShowDialog();
                }
                var mess = new MessageDialog() { tbl_Title = { Text = "Bảo hành" }, tbl_Message = { Text = $"sản phẩm bảo hành đến hết ngày {pig.ExpirationDate.ToShortDateString()}" } };
                mess.ShowDialog();
            }
        }
    }
}
