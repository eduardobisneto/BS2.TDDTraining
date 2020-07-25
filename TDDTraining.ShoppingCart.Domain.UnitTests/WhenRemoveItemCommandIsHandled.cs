using System;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenRemoveItemCommandIsHandled : WhenHandlingCartCommand<RemoveItemCommand, RemoveItemCommandHandler>
    {
        [Fact]
        public void ProductIsNotPresentInCart()
        {
            var productId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            GivenProductAlreadyExistsInCart(productId, customerId);

            var command = new RemoveItemCommand(customerId, productId);

            var cart = ((OkResult<Cart>)WhenCommandIsHandled(command)).Body;
            
            Assert.DoesNotContain(cart.Itens, x => x.ProductId == command.ProductId);
        }
        
        [Fact]
        public void IfCartDoesNotExistsCommandDoesNotFailAndNewCartIsCreatedForCustomer()
        {
            var command = new RemoveItemCommand(Guid.NewGuid(), Guid.NewGuid());
            
            var cart = ((OkResult<Cart>)WhenCommandIsHandled(command)).Body;
            
            AssertNewCartWasCreatedToTheCustomer(cart, command);
        }

        private static void AssertNewCartWasCreatedToTheCustomer(Cart cart, RemoveItemCommand command)
        {
            Assert.NotNull(cart);
            Assert.Equal(command.CustomerId, cart.CustomerId);
        }

        protected override RemoveItemCommandHandler CreateCommandHandler()
        {
            return new RemoveItemCommandHandler(Repository);
        }
    }
}