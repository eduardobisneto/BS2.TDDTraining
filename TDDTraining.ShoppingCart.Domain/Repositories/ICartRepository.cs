using System;

namespace TDDTraining.ShoppingCart.Domain.Repositories
{
    public interface ICartRepository
    {
        Cart? GetByCustomerId(Guid customerId);
        void Save(Cart cart);
    }
}