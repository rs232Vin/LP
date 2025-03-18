using BusinessEntities;
using Common;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : InMemoryRepository<Order>, IOrderRepository
    {
        public IEnumerable<Order> GetOrders(string productName = null, Guid? productId = null)
        {
            if (Items != null && Items.Any())
            {
                var result = Items;

                if (string.IsNullOrEmpty(productName))
                {
                    result = result.Where(k => k.LineItems != null
                                                && k.LineItems.Any(l => l.Product != null
                                                                    && string.Equals(l.Product.Name, productName)));
                }

                if(productId.HasValue)
                {
                    result = result.Where(k => k.LineItems != null 
                                            && k.LineItems.Any(l => l.Product != null
                                                && l.Product.Id == productId.Value));
                }

                return result;
            }

            return null;
        }
    }
}