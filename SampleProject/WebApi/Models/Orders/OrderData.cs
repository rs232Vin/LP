using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public IList<LineItemData> LineItems { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderData(Order order) : base(order)
        {
            if (order.LineItems != null && order.LineItems.Any())
            {
                LineItems = new List<LineItemData>();
                foreach (var item in order.LineItems)
                {
                    LineItems.Add(new LineItemData(item));
                }
                TotalAmount = order.CalculateTotalAmount();
            }
        }
    }
}