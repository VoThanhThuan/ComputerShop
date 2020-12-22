using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class Product : IEnumerable
    {
        public Guid ID { set; get; }
        public string Name { get; set; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public DateTime DateCreated { set; get; }
        public string SeriNumber { get; set; }
        public string ImagePath { get; set; }
        public ProductGuarantee ProductGuarantee { get; set; }

        public ProductInImport ProductInImports { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
