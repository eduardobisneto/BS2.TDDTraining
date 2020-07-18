using System;
using System.Linq;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenAddItemCommandIsHandled : WhenHandlingCartCommand<AddItemCommand, AddItemCommandHandler, Cart>
    {
        [Fact]
        public void ItemShouldBePresentInTheCartWhitQuantityOfOne()
        {
            var command = new AddItemCommand(Guid.NewGuid(), Guid.NewGuid());
            var cart = WhenCommandIsHandled(command);
            
            Assert.Contains(cart.Itens, x => x.ProductId == command.ProductId);
            var item = cart.Itens.Single(x => x.ProductId == command.ProductId);
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public void CartShouldMatchCustomerId()
        {
            var command = new AddItemCommand(Guid.NewGuid(), Guid.NewGuid());
            var cart = WhenCommandIsHandled(command);
            Assert.Equal(command.CustomerId, cart.CustomerId);
        }

        [Fact]
        public void IfCustomerAlreadyHaveACartItShouldBeUsed()
        {
            var customerId = Guid.NewGuid();
            var existingCart = AssumeCartAlreadyExists(customerId);

            var command = new AddItemCommand(customerId, Guid.NewGuid());

            var cart = WhenCommandIsHandled(command);
            
            Assert.Equal(existingCart.Id, cart.Id);
        }

        [Fact]
        public void IfProductExistsItsQuantityShouldIncrease()
        {
            var productId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            GivenProductAlreadyExistsInCart(productId, customerId);
            
            var command = new AddItemCommand(customerId, productId);
            var cart = WhenCommandIsHandled(command);

            var item = cart.Itens.Single(x => x.ProductId == productId);
            Assert.Equal(2, item.Quantity);
        }

        protected override AddItemCommandHandler CreateCommandHandler()
        {
            return new AddItemCommandHandler(Repository);
        }
    }
}