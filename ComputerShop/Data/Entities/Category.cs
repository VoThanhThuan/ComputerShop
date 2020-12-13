using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.Entities.Enums;

namespace Dashboard.Data.Entities
{
    public class Category
    {
        public int ID { set; get; }
        public string Name { get; set; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; }
        public Status Status { set; get; }

        public List<ProductInCategory> ProductInCategories { get; set; }

    }
}
