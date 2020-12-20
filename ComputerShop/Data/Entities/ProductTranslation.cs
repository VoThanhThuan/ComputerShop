using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class ProductTranslation
    {
        public int ID { set; get; }
        public int Amount { get; set; }
        public Guid TransactionID { set; get; }
        public Transaction Transaction { get; set; }
        public Guid ProductId { set; get; }
        public Product Product { get; set; }

    }
}
