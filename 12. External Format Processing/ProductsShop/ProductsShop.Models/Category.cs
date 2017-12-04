using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductsShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [MaxLength(15 , ErrorMessage = "Category name should be 15 symbols or less")]
        [MinLength(3, ErrorMessage = "Category name should be more than 2 symbols")]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();
    }
}
