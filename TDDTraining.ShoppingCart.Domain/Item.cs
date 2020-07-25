using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Item
    {
        public Guid ProductId { get; }
        public int Quantity { get; private set; }
        public decimal ProductPrice { get; }

        public Item(Guid productId, decimal productPrice)
        {
            ProductId = productId;
            ProductPrice = productPrice;
            Quantity = 1;
        }

        public void IncreaseQuantity()
        {
            Quantity++;
        }
    }
}