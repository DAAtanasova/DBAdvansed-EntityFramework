using System;
using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class User
    {
        public int UserId { get; set; }

        //optional
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int? Age { get; set; }

        public ICollection<Product> BuyProducts { get; set; } = new List<Product>();

        public ICollection<Product> SellProducts { get; set; } = new List<Product>();
    }
}
