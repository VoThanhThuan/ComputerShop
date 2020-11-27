using System;
using System.Collections.Generic;
using System.Text;
using WebShopAccessories.Data.Entities.Enums;

namespace Dashboard.Data.Entities
{
    public class Promotion
    {
        public int Id { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public bool ApplyForAll { set; get; }
        public int? DiscountPercent { set; get; }
        public decimal? DiscountAmount { set; get; }
        public string ProductIds { set; get; }
        public string ProductCategoryIds { set; get; } 
        public string Name { set; get; }

    }
}
