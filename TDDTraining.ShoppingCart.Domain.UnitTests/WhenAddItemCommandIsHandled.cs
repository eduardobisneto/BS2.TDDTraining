using System;
using System.Linq;
using Moq;
using TDDTraining.ShoppingCart.Domain.Apis;
using TDDTraining.ShoppingCart.Domain.CommandHandlers;
using TDDTraining.ShoppingCart.Domain.Commands;
using TDDTraining.ShoppingCart.Domain.Core;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{

    public class WhenAddItemCommandIsHandled : WhenHandlingCartCommand<AddItemCommand, AddItemCommandHandler>
    {
        private readonly Mock<IProductApi> productApiStub;
        private readonly Mock<ILogger> loggerMock;

        public WhenAddItemCommandIsHandled()
        {
            productApiStub = CreateProductApiStub();
            loggerMock = CreateLoggerMock();
        }

        [Fact]
        public void CartShouldContainsTheAddedItemWithQuantityOfOne()
        {
            var command = new AddItemCommand(Guid.NewGuid(), Guid.NewGuid());
            var cart = WhenCommandIsHandled<OkResult<Cart>>(command).Body;
            var item = cart.Itens.Single(x => x.ProductId == command.ProductId);
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public void CartShouldHaveCustomerId()
        {
            var command = new AddItemCommand(Guid.NewGuid(), Guid.NewGuid());
            var cart = WhenCommandIsHandled<OkResult<Cart>>(command).Body;
            Assert.Equal(command.CustomerId, cart.Customer.Id);
        }

        [Fact]
        public void IfCustomerAlreadyHaveACartItShouldBeUsed()
        {
            var customerId = Guid.NewGuid();
            var exitingCart = AssumeCartAlreadyExists(customerId);
            
            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(customerId, Guid.NewGuid())).Body;
            
            Assert.Equal(exitingCart.Id, cart.Id);
        }

        [Fact]
        public void IfProductExistsItsQuantityShouldIncrease()
        {
            var productId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            GivenProductAlreadyExistsInCart(productId, customerId);
            
            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(customerId, productId)).Body;

            var item = cart.Itens.Single(x => x.ProductId == productId);
            
            Assert.Equal(2, item.Quantity);
        }

        [Fact]
        public void AddedProductShouldHaveTheRightProductInfo()
        {
            var nikeShoes = new NikeShoes();

            AssumeProductInfoIs(nikeShoes);

            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(Guid.NewGuid(), nikeShoes.ProductId)).Body;

            var item = cart.Itens.Single(x => x.ProductId == nikeShoes.ProductId);
            Assert.Equal(nikeShoes.Name, item.ProductName);
            Assert.Equal(nikeShoes.Price, item.ProductPrice);
        }

        [Fact]
        public void CartTotalShouldBeSumOfItems()
        {
            var nikeShoes = new NikeShoes();
            
            AssumeProductInfoIs(nikeShoes);

            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(Guid.NewGuid(), nikeShoes.ProductId))
                .Body;
            
            Assert.Equal(nikeShoes.Price, cart.Total);
        }

        [Fact]
        public void CartTotalShouldBeSumOfItemsTimesQuantities()
        {
            var nikeShoes = new NikeShoes();
            
            AssumeProductInfoIs(nikeShoes);

            var customerId = Guid.NewGuid();
            
            WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(customerId, nikeShoes.ProductId));
            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(customerId, nikeShoes.ProductId)).Body;
            
            Assert.Equal(200, cart.Total);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void IfProductApiFailsProductShouldBeAddedToCart(int numberOfFailures)
        {
            var productId = Guid.NewGuid();
            AssumeProductApiFailsFor(productId, numberOfFailures);

            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(Guid.NewGuid(), productId)).Body;

            Assert.Contains(cart.Itens, x => x.ProductId == productId);
        }

        [Fact]
        public void IfProductApiIsUnavailableShouldReturnErrorResult()
        {
            var dummy = new Dummy();
            AssumeProductApiFails();
        
            var error = WhenCommandIsHandled<ServiceUnavailableError>(new AddItemCommand(Guid.NewGuid(), dummy.ProductId));
            
            Assert.NotNull(error);
        }
        
        [Fact]
        public void IfProductApiIsUnavailableProblemShouldBeLogged()
        {
            AssumeProductApiFails();
            
            WhenCommandIsHandled<ServiceUnavailableError>(new AddItemCommand(Guid.NewGuid(), Guid.NewGuid()));

            loggerMock.Verify(x => x.LogError(It.IsAny<Exception>()));
        }

        [Fact]
        public void IfProductDoesNotExistShouldReturnErrorResult()
        {
            var nonExistentProduct = new NonExistentProduct();
            AssumeProductDoesNotExist(nonExistentProduct);

            var error = WhenCommandIsHandled<ProductDoesNotExistError>(new AddItemCommand(Guid.NewGuid(),
                nonExistentProduct.ProductId));
            
            Assert.NotNull(error);
        }

        private void AssumeProductDoesNotExist(NonExistentProduct nonExistentProduct)
        {
            productApiStub
                .Setup(x => x.GetProduct(nonExistentProduct.ProductId))
                .Returns((Apis.ProductInfo) null);
        }

        private void AssumeProductApiFails()
        {
            productApiStub
                .Setup(x => x.GetProduct(It.IsAny<Guid>()))
                .Throws<Exception>();
        }

        private void AssumeProductApiFailsFor(Guid productId, int numberOfFailures)
        {
            var setupSequence = productApiStub
                .SetupSequence(x => x.GetProduct(productId));

            for (var i = 0; i < numberOfFailures; i++)
            {
                setupSequence.Throws<Exception>();   
            }
            
            setupSequence.Returns(ProductInfoBuilder.For<Dummy>().Build());
        }

        private void AssumeProductInfoIs(WellKnownProduct wellKnownProduct)
        {
            productApiStub
                .Setup(x => x.GetProduct(wellKnownProduct.ProductId))
                .Returns(new ProductInfoBuilder(wellKnownProduct).Build());
        }
        
        protected override AddItemCommandHandler CreateCommandHandler()
        {
            return new AddItemCommandHandler(Repository, 
                                             productApiStub.Object, 
                                             RetryStrategy.CreateAddItemCommandRetryStrategy(),
                                             loggerMock.Object);
        }
    }
}