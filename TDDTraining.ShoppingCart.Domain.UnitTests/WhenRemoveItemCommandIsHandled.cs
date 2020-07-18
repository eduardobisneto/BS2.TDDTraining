using System;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenRemoveItemCommandIsHandled : WhenHandlingCartCommand<RemoveItemCommand, RemoveItemCommandHandler, Cart>
    {
        [Fact]
        public void ProductIsNotPresentInCart()
        {
            var productId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            GivenProductAlreadyExistsInCart(productId, customerId);

            var command = new RemoveItemCommand(customerId, productId);

            var cart = WhenCommandIsHandled(command);
            
            Assert.DoesNotContain(cart.Itens, x => x.ProductId == command.ProductId);
        }

        [Fact]
        public void IfCartDoesNotExitsCommandDoesNotFail()
        {
            var command = new RemoveItemCommand(Guid.NewGuid(), Guid.NewGuid());

            WhenCommandIsHandled(command);
        }

        protected override RemoveItemCommandHandler CreateCommandHandler()
        {
            return new RemoveItemCommandHandler(Repository);
        }
    }
}