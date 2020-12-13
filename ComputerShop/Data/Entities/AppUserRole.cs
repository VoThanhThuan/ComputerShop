using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Dashboard.Data.Entities
{
    [NotMapped]
    public class AppUserRole
    {
        public string UserID { get; set; }
        public string RoleID { get; set; }

    }
}
