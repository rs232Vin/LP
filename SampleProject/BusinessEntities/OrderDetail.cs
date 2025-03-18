using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class OrderDetail : IdObject
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price
        {
            get
            {
                if(Product == null || Quantity == 0)
                    return 0;

                return Product.Price.Value * Quantity;
            }
        }
    }
}