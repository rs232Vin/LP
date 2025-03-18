using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }

        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Category = product.Category;
            Price = product.Price;
        }
    }
}