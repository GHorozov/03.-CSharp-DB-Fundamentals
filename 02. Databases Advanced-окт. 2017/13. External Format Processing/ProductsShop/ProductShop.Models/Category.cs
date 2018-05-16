namespace ProductShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int CategoryId { get; set; }
        [MinLength(3)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> Products { get; set; } = new List<CategoryProduct>();
    }
}
