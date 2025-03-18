using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Orders
{
    public class LineItemData : IdObjectData
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public LineItemData(OrderDetail lineItem) : base(lineItem)
        {
            ProductId = lineItem.Product.Id;
            ProductName = lineItem.Product.Name;
            Quantity = lineItem.Quantity;
        }
    }
}