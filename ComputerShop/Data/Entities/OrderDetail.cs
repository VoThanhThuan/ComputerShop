﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class OrderDetail
    {

        public int OrderID { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Order { get; set; }
        public Guid ProductID { set; get; }

        public Product Product { get; set; }

    }
}
