using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Item
    {
        public Guid ProductId { get; }
        public int Quantity { get; private set; }

        public Item(Guid productId)
        {
            ProductId = productId;
            Quantity = 1;
        }

        public void IncreaseQuantity()
        {
            Quantity++;
        }
    }
}