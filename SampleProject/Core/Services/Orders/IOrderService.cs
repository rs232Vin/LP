using BusinessEntities;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    public interface IOrderService
    {
        Order CreateOrder(OrderModel order);
        void DeleteOrder(Guid id);
        Order GetOrder(Guid id);
        IEnumerable<Order> GetOrders(string productName = null, Guid? productId = null);
        Order UpdateOrder(Guid id, OrderModel order);
    }
}