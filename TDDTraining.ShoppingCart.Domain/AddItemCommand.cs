using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class AddItemCommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public AddItemCommand(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}