using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Dashboard.Data.Entities
{
    [NotMapped]
    public class AppUserRole : IdentityUserRole<Guid>
    {
        //public Guid UserId { get; set; }
        //public Guid RoleId { get; set; }

    }
}
