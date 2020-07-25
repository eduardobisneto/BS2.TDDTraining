using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class ProductInfo
    {
        public Guid ProductId { get; }
        public decimal Price { get; }

        public ProductInfo(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
    }
}