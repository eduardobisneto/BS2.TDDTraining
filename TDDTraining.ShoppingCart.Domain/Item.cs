using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Item
    {
        public Guid ProductId { get; }
        public int Quantity { get; private set; }
        public decimal ProductPrice { get; }
        public string ProductName { get; }
        public decimal Total => Quantity * ProductPrice;

        public Item(Guid productId, string productName, decimal productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = 1;
        }

        public void IncreaseQuantity()
        {
            Quantity++;
        }
    }
}