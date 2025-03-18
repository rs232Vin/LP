using BusinessEntities;
using Common;
using Core.Factories;
using Data.DTO;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IIdObjectFactory<Order> _orderFactory;
        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository, IIdObjectFactory<Order> orderFactory)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
        }

        public Order CreateOrder(OrderModel order)
        {
            Order orderToCreate = null;

            if (order != null && order.LineItems != null && order.LineItems.Any()) 
            {
                var existingOrder = _orderRepository.Get(order.Id);
                if (existingOrder != null)
                {
                    throw new Common.Exceptions.AlreadyExistsException("Order with this id already exists.");
                }

                if(order.LineItems == null || !order.LineItems.Any())
                {
                    throw new Common.Exceptions.ValidationException("Cannot create order. Products missing.");
                }

                foreach(var lineItem in order.LineItems.Where(l => l != null && l.Quantity > 0))
                {   
                    Product product = _productRepository.Get(lineItem.ProductId);
                    if(product != null)
                    {
                        if(orderToCreate == null)
                        {
                            orderToCreate = _orderFactory.Create(order.Id);
                            orderToCreate.LineItems = new List<OrderDetail>();
                        }
                        OrderDetail lineItemToCreate = new OrderDetail();
                        lineItemToCreate.Product = product;
                        lineItemToCreate.Quantity = lineItem.Quantity;
                        orderToCreate.LineItems.Add(lineItemToCreate);
                    }
                }

                if(orderToCreate != null)
                    _orderRepository.Save(orderToCreate);
            }

            if (orderToCreate == null)
                throw new Common.Exceptions.ValidationException("Incorrect product details.");

            return orderToCreate;
        }

        public void DeleteOrder(Guid id)
        {
            var existingOrder = _orderRepository.Get(id);
            if (existingOrder == null)
            {
                throw new Common.Exceptions.NotFoundException("Order not found.");
            }

            _orderRepository.Delete(existingOrder);
        }

        public Order GetOrder(Guid id)
        {
            return _orderRepository.Get(id);
        }

        public IEnumerable<Order> GetOrders(string productName = null, Guid? productId = null)
        {
            return _orderRepository.GetOrders(productName, productId);
        }

        public Order UpdateOrder(Guid id, OrderModel order)
        {
            if(order == null)
            {
                throw new Common.Exceptions.ValidationException("Cannot update order. Invalid order details.");
            }

            Order orderToUpdate = _orderRepository.Get(id);
            if (orderToUpdate == null)
                throw new Common.Exceptions.NotFoundException("Order not found.");

            if(order.LineItems == null || !order.LineItems.Any())
                throw new Common.Exceptions.ValidationException("Product details missing.");

            bool hasUpdates = false;
            orderToUpdate.LineItems.Clear();
            foreach(var lineItem in order.LineItems.Where(l => l != null && l.ProductId != null))
            {
                if (lineItem.Quantity > 0)
                {
                    hasUpdates = true;
                    Product product = _productRepository.Get(lineItem.ProductId);
                    if (product != null)
                    {
                        orderToUpdate.LineItems.Add(new OrderDetail
                        {
                            Product = product,
                            Quantity = lineItem.Quantity,
                        });
                    }
                }
            }

            if (hasUpdates)
            {
                _orderRepository.Save(orderToUpdate);
                return orderToUpdate;
            }
            else
            {
                throw new Common.Exceptions.ValidationException("Cannot update order. Invalid order details.");
            }     
        }
    }
}