using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class ProductInImport
    {
        public int ProductID { get; set; }

        public Product Product { get; set; }

        public int ImportID { get; set; }

        public Import Import { get; set; }
    }
}
