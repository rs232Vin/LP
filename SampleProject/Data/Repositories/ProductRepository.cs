using BusinessEntities;
using Common;
using Raven.Abstractions.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : InMemoryRepository<Product>, IProductRepository
    {
        public IEnumerable<Product> GetAllProducts()
        {
            if (Items != null && Items.Any())
            {
                return Items;
            }

            return null;
        }

        public IEnumerable<Product> GetProducts(string name = null, string category = null)
        {
            if(Items != null && Items.Any())
            {
                var result = from product in Items
                             select product;
                if (!string.IsNullOrEmpty(name))
                    result = result.Where(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));

                if(!string.IsNullOrEmpty(category))
                    result = result.Where(i => string.Equals(i.Category, category, StringComparison.OrdinalIgnoreCase));

                return result;
            }

            return null;
        }

        //public Product CreateProduct(Guid id, string name, string category, decimal? price)
        //{
        //    var product = Get(id);
        //    if (product != null)
        //    {
        //        throw new Common.Exceptions.AlreadyExistsException("Product with this id already exists.");
        //    }

        //    Product newProduct = new Product();
        //    newProduct.SetName(name);
        //    newProduct.SetCategory(category);
        //    newProduct.SetPrice(price);            
        //    Save(newProduct);
        //    return newProduct;
        //}
    }
}