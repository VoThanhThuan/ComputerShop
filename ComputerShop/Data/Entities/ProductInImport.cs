using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class ProductInImport
    {
        public int ID { get; set; }
        public Guid ProductID { get; set; }

        public Product Product { get; set; }

        public Guid ImportID { get; set; }

        public Import Import { get; set; }
    }
}
