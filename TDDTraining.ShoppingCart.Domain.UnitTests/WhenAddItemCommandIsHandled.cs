using System;
using System.Linq;
using Moq;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenAddItemCommandIsHandled : WhenHandlingCartCommand<AddItemCommand, AddItemCommandHandler, Cart>
    {
        private readonly Mock<IProductApi> productApiStub;

        public WhenAddItemCommandIsHandled()
        {
            productApiStub = CreateProductApiStub();
        }

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

        [Fact]
        public void AddedProductShouldHaveTheRightProductInfo()
        {
            var productInfo = ProductInfoBuilder.For<NikeShoes>().Build();

            AssumeProductInfoIs(productInfo);

            var cart = WhenCommandIsHandled(new AddItemCommand(Guid.NewGuid(), productInfo.ProductId));

            var item = cart.Itens.Single(x => x.ProductId == productInfo.ProductId);
            
            Assert.Equal(productInfo.ProductName, item.ProductName);
            Assert.Equal(productInfo.Price, item.ProductPrice);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ProductShouldBeAddedEvenIfProductApiFailsXTimes(int numberOfFailures)
        {
            var productId = Guid.NewGuid();

            AssumeProductApiWillFail(productId, numberOfFailures);

            var cart = WhenCommandIsHandled(new AddItemCommand(Guid.NewGuid(), productId));
            
            Assert.Contains(cart.Itens, x => x.ProductId == productId);
        }

        private void AssumeProductApiWillFail(Guid productId, int numberOfFailures)
        {
            var setupSequence = productApiStub
                .SetupSequence(x => x.GetProduct(productId));

            for (var i = 0; i < numberOfFailures; i++)
            {
                setupSequence = setupSequence
                    .Throws<Exception>();
            }

            setupSequence
                .Returns(ProductInfoBuilder.For<Dummy>().Build());
        }

        private void AssumeProductInfoIs(ProductInfo productInfo)
        {
            productApiStub
                .Setup(x => x.GetProduct(productInfo.ProductId))
                .Returns(productInfo);
        }

        protected override AddItemCommandHandler CreateCommandHandler()
        {
            return new AddItemCommandHandler(Repository, productApiStub.Object, RetryStrategy.CreateRetryStrategy());
        }
    }
}