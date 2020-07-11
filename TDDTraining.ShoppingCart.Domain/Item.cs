using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Item
    {
        public Guid ProductId { get; }

        public Item(Guid productId)
        {
            ProductId = productId;
        }
    }
}