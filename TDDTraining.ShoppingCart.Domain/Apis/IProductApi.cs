using System;

namespace TDDTraining.ShoppingCart.Domain.Apis
{
    public interface IProductApi
    {
        ProductInfo GetProduct(Guid productId);
    }
}