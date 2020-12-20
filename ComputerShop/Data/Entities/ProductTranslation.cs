using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class ProductTranslation
    {
        public int ID { set; get; }
        public int TransactionID { set; get; }
        public Transaction Transaction { get; set; }
        public int ProductId { set; get; }
        public Product Product { get; set; }

    }
}
