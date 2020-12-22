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

namespace Dashboard.Staff
{
    /// <summary>
    /// Interaction logic for PageUCSaleHistory.xaml
    /// </summary>
    public partial class PageUCSaleHistory : UserControl
    {
        public PageUCSaleHistory()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {

            var listBill = Db.Context.Transactions.Select(x => x).ToList();
            listBill.Reverse();
            foreach (var bill in listBill)
            {
                var query = from pt in Db.Context.ProductTranslations
                    join p in Db.Context.Products on pt.ProductId equals p.ID
                    where pt.TransactionID == bill.ID
                    select new {p.ID, p.Name, p.Price, pt.Amount};
                var expander = new UserControl_SalesHistory()
                {
                    DtgProduct = { ItemsSource = query.ToList()},
                    TbxID = {Text = $"{bill.ID}"},
                    TbxAmount = {Text = $"{bill.Amount}"},
                    TbxDateTransaction = {Text = $"{bill.TransactionDate}"},
                    TbxFee = {Text = $"{bill.Fee}"}
                };
                RenderPages.Children.Add(expander);
            }
            
        }

    }
}
