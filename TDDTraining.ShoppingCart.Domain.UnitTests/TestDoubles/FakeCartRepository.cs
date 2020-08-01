using System;
using System.Collections.Generic;
using System.Linq;
using TDDTraining.ShoppingCart.Domain.Repositories;

namespace TDDTraining.ShoppingCart.Domain.UnitTests.TestDoubles
{
    public class FakeCartRepository : ICartRepository
    {
        private readonly List<Cart> carts = new List<Cart>();
        
        public Cart GetByCustomerId(Guid customerId)
        {
            return carts.SingleOrDefault(x => x.Customer.Id == customerId);
        }

        public void Save(Cart cart)
        {
            carts.Add(cart);
        }
    }
}