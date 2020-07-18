using System;
using TDDTraining.ShoppingCart.Domain.UnitTests.Core;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public abstract class WhenHandlingCartCommand<TCommand, TCommandHandler, TResult> : WhenHandlingCommand<TCommand, TCommandHandler, TResult>
        where TCommandHandler : IHandleCommand<TCommand, TResult>
    {
        protected void GivenProductAlreadyExistsInCart(Guid productId, Guid customerId)
        {
            new AddItemCommandHandler(Repository).Handle(new AddItemCommand(customerId, productId));
        }
        
        protected Cart AssumeCartAlreadyExists(Guid customerId)
        {
            return new AddItemCommandHandler(Repository).Handle(new AddItemCommand(customerId, Guid.NewGuid()));
        }
    }
}