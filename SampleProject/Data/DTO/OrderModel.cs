using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public IList<OrderDetailModel> LineItems { get; set; }
    }
}
