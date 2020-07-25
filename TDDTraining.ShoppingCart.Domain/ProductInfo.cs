using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class ProductInfo
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public decimal Price { get; }

        public ProductInfo(Guid productId, string productName, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
        }
    }
}