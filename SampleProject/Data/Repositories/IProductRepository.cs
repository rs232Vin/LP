using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProducts(string name = null, string category = null);
        IEnumerable<Product> GetAllProducts();
        //Product CreateProduct(Guid id, string name, string category, decimal? price);
        void DeleteAll();
    }
}