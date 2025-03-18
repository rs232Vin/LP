using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    [AutoRegister]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IIdObjectFactory<Product> _productFactory;

        public ProductService(IProductRepository productRepository, IIdObjectFactory<Product> productFactory)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
        }
        public Product AddNewProduct(Guid id, string name, string category, decimal? price)
        {
            var existingProduct = _productRepository.Get(id);
            if(existingProduct != null)
            {
                throw new Common.Exceptions.AlreadyExistsException("Product with this id already exists.");
            }

            var product = _productFactory.Create(id);
            product.SetName(name);
            product.SetCategory(category);
            product.SetPrice(price);
            _productRepository.Save(product);
            return product;
            //return _productRepository.CreateProduct(id, name, category, price);
        }

        public void DeleteAllProducts()
        {
            _productRepository.DeleteAll();
        }

        public void DeleteProduct(Guid id)
        {
            Product product = _productRepository.Get(id);
            if(product == null)
                throw new Common.Exceptions.NotFoundException("Product not found.");

            _productRepository.Delete(product);
        }

        public Product UpdateProduct(Guid id, string name,string category, decimal? price)
        {
            Product product = _productRepository.Get(id);
            if (product == null)
                throw new Common.Exceptions.NotFoundException("Product not found.");

            product.SetName(name);
            product.SetCategory(category);
            product.SetPrice(price);
            _productRepository.Save(product);
            return product;
        }

        public Product GetProduct(Guid id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<Product> GetProducts(string name = null, string category = null)
        {
            return _productRepository.GetProducts(name, category);
        }
    }
}