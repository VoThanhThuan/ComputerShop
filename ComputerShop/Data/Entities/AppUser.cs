using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class AppUser 
    {
        public string ID { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Identity { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime Dob { get; set; }
        public string Avatar { get; set; }

        public List<Import> ImportGoods { get; set; }

        public List<Cart> Carts { get; set; }

        public List<Order> Orders { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
