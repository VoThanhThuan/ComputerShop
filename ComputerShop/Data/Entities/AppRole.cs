using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Dashboard.Data.Entities
{
    public class AppRole 
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
