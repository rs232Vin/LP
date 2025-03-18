using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public IList<OrderDetail> LineItems { get; set; }

        public decimal CalculateTotalAmount()
        {
            if(LineItems == null || !LineItems.Any())
            {
                return 0;
            }

            decimal totalAmount = 0;
            foreach (var item in LineItems)
            {
                totalAmount += item.Price;
            }

            return totalAmount;
        }
    }
}