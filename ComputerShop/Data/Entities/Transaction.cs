using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Dashboard.Data.Entities
{
    public class Transaction
    {
        public Guid ID { set; get; }
        public DateTime TransactionDate { set; get; }
        public decimal Amount { set; get; }
        public decimal Fee { set; get; }
        public string NameStaff { get; set; }
        public List<ProductTranslation> ProductTranslations { get; set; }
    }
}
