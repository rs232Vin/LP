using Common.Exceptions;
using Core.Services.Products;
using Data.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.Models.Errors;
using WebApi.Models.Products;
using WebApi.Utility;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            ApiResponse<ProductData> response = new ApiResponse<ProductData>();
            try
            {
                var newProduct = _productService.AddNewProduct(productId, model.Name,model.Category, model.Price);
                response.Success = true;
                response.Data = new ProductData(newProduct);
            }
            catch (Common.Exceptions.AlreadyExistsException alreadyExistsEx)
            {
                // Log Error
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP005, alreadyExistsEx.Message);
                response.ApplicationErrors.Add(error);
            }
            catch (Common.Exceptions.ValidationException validationEx)
            {
                // Log Error
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP003, validationEx.Message);
                response.ApplicationErrors.Add(error);
            }
            return Found(response);
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody]ProductModel model)
        {
            ApiResponse<ProductData> response = new ApiResponse<ProductData>();
            try
            {
                var product = _productService.UpdateProduct(productId, model.Name, model.Category, model.Price);
                response.Success = true;
                response.Data = new ProductData(product);
            }
            catch (Common.Exceptions.NotFoundException notFoundEx)
            {
                // Log Error
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP006, notFoundEx.Message);
                response.ApplicationErrors.Add(error);
            }
            catch (Common.Exceptions.ValidationException validationEx)
            {
                // Log Error
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP003, validationEx.Message);
                response.ApplicationErrors.Add(error);
            }

            return Found(response);
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                _productService.DeleteProduct(productId);
                response.Success = true;
                response.Data = "Product deleted.";
            }
            catch (NotFoundException notFoundEx)
            {
                // Log Error
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP006, notFoundEx.Message);
                response.ApplicationErrors.Add(error);
            }

            return Found(response);
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _productService.GetProduct(productId);
            if(product == null)
            {
                ApiResponse<string> response = new ApiResponse<string>();
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP006, "Product not found.");
                response.ApplicationErrors.Add(error);
                return Found(response);
            }
            else
            {
                // Log Error
                ApiResponse<ProductData> response = new ApiResponse<ProductData>();
                response.Success = true;
                response.Data = new ProductData(product);
                return Found(response);
            }
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(string name = null, string category = null)
        {
            ApiResponse<IEnumerable<ProductData>> response = new ApiResponse<IEnumerable<ProductData>>();
            var products = _productService.GetProducts(name, category);
            response.Success = true;
            response.Data = products != null ? products.Select(p => new ProductData(p)) : null;
            return Found(response);
        }
    }
}