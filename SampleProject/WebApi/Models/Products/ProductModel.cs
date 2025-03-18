using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
    }
}