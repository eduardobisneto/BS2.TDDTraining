using System;
using TDDTraining.ShoppingCart.Domain.CommandHandlers;
using TDDTraining.ShoppingCart.Domain.Commands;
using TDDTraining.ShoppingCart.Domain.Core;
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
            var cart = WhenCommandIsHandled<OkResult<Cart>>(command).Body;
            
            Assert.DoesNotContain(cart.Itens, x => x.ProductId == productId);
        }

        [Fact]
        public void IfCartDoesNotExistsCommandDoesNotFailAndNewCartIsCreatedForCustomer()
        {
            var command = new RemoveItemCommand(Guid.NewGuid(), Guid.NewGuid());
            
            WhenCommandIsHandled<OkResult<Cart>>(command);

            // como seria do ponto de vista de um teste de integracao
            var cart = Repository.GetByCustomerId(command.CustomerId);
            
            AssertNewCartWasCreatedToTheCustomer(cart, command);
        }

        [Fact]
        public void EmptyCartTotalShouldBeZero()
        {
            var cart = WhenCommandIsHandled<OkResult<Cart>>(new RemoveItemCommand(Guid.NewGuid(), Guid.NewGuid())).Body;

            Assert.Equal(0, cart.Total);
        }

        private static void AssertNewCartWasCreatedToTheCustomer(Cart cart, RemoveItemCommand command)
        {
            Assert.NotNull(cart);
            Assert.Equal(command.CustomerId, cart.Customer.Id);
        }

        protected override RemoveItemCommandHandler CreateCommandHandler()
        {
            return new RemoveItemCommandHandler(Repository);
        }
    }
}