using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class Cart
    {
        public int ID { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public string UserID { get; set; }

        public Product Product { get; set; }

        public DateTime DateCreated { get; set; }

        public AppUser AppUser { get; set; }
    }
}
