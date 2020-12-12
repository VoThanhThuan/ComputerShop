using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class ProductTranslation
    {
        public int ID { set; get; }
        public int ProductId { set; get; }
        public string Name { set; get; }
        public string Details { set; get; }
        public Product Product { get; set; }

    }
}
