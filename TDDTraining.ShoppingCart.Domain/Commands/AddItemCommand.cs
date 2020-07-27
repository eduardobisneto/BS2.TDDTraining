using System;

namespace TDDTraining.ShoppingCart.Domain.Commands
{
    public class AddItemCommand
    {
        public Guid ProductId { get; }
        public Guid CustomerId { get; }

        public AddItemCommand(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}