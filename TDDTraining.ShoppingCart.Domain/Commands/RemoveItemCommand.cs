using System;

namespace TDDTraining.ShoppingCart.Domain.Commands
{
    public class RemoveItemCommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }

        public RemoveItemCommand(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}