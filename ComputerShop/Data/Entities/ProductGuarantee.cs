using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class ProductGuarantee
    {
        public int ID { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SeriNumber { get; set; }
        public string Description { get; set; }
        public Guid ProductId { set; get; }
        public Product Product { get; set; }

    }
}
