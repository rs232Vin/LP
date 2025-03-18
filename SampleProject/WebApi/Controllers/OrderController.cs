using BusinessEntities;
using Common.Exceptions;
using Core.Services.Orders;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using WebApi.Models;
using WebApi.Models.Errors;
using WebApi.Models.Orders;
using WebApi.Models.Products;
using WebApi.Utility;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            ApiResponse<OrderData> response = new ApiResponse<OrderData>();
            model.Id = orderId;
            try
            {
                var newOrder = _orderService.CreateOrder(model);
                response.Success = true;
                response.Data = new OrderData(newOrder);
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

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                _orderService.DeleteOrder(orderId);
                response.Success = true;
                response.Data = "Order deleted.";
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

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _orderService.GetOrder(orderId);
            if (order == null)
            {
                // Log Error
                ApiResponse<string> response = new ApiResponse<string>();
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP006, "Order not found.");
                response.ApplicationErrors.Add(error);
                return Found(response);
            }
            else
            {
                ApiResponse<OrderData> response = new ApiResponse<OrderData>();
                response.Success = true;
                response.Data = new OrderData(order);
                return Found(response);
            }
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(string productName = null, Guid? productId = null)
        {
            ApiResponse<IEnumerable<OrderData>> response = new ApiResponse<IEnumerable<OrderData>>();
            var orders = _orderService.GetOrders(productName, productId);
            response.Success = true;
            response.Data = orders != null ? orders.Select(o => new OrderData(o)) : null;
            return Found(response);
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody]OrderModel model)
        {
            ApiResponse<OrderData> response = new ApiResponse<OrderData>();
            try
            {
                var order = _orderService.UpdateOrder(orderId, model);
                response.Success = true;
                response.Data = new OrderData(order);
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
    }
}