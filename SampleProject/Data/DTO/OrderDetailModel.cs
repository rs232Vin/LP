using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class OrderDetailModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}