using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public interface IProductApi
    {
        ProductInfo GetProduct(Guid productId);
    }
}