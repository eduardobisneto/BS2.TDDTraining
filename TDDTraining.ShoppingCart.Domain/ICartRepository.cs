using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public interface ICartRepository
    {
        Cart? GetByCustomerId(Guid customerId);
        void Save(Cart cart);
    }
}