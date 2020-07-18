using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDTraining.ShoppingCart.Domain.UnitTests.TestDoubles
{
    internal class FakeCartRepository : ICartRepository
    {
        private IList<Cart> carts = new List<Cart>();
        
        public Cart GetByCustomerId(Guid customerId)
        {
            return carts.SingleOrDefault(x => x.CustomerId == customerId);
        }

        public void Save(Cart cart)
        {
            carts.Add(cart);
        }
    }
}