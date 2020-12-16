using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class Import
    {
        public int ID { get; set; }
        public DateTime DayImport { get; set; }
        public string Supplier { get; set; }
        public string Warehouse { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public Guid SecurityCode { get; set; }
        public List<ProductInImport> ProductInImports { get; set; }

    }
}
