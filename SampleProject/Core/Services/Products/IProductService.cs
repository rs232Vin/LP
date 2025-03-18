using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IProductService
    {
        Product AddNewProduct(Guid id, string name, string category, decimal? price);

        void DeleteProduct(Guid id);

        void DeleteAllProducts();

        Product UpdateProduct(Guid id, string name, string category, decimal? price);

        Product GetProduct(Guid id);

        IEnumerable<Product> GetProducts(string name = null, string category = null);
    }
}
