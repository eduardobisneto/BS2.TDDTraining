using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TDDTraining.ShoppingCart.Domain.Apis;
using TDDTraining.ShoppingCart.Domain.CommandHandlers;
using TDDTraining.ShoppingCart.Domain.Commands;
using TDDTraining.ShoppingCart.Domain.Core;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{

    public class WhenAddItemCommandIsHandled : WhenHandlingCartCommand<AddItemCommand, AddItemCommandHandler>
    {
        private readonly ITestOutputHelper stdout;
        private readonly Mock<IProductApi> productApiStub;
        private readonly Mock<ILogger> loggerMock;

        public WhenAddItemCommandIsHandled(ITestOutputHelper stdout)
        {
            this.stdout = stdout;
            productApiStub = CreateProductApiStub();
            loggerMock = CreateLoggerMock();
        }

        [Fact]
        public void CartShouldContainsTheAddedItemWithQuantityOfOne()
        {
            var command = new AddItemCommand(Guid.NewGuid(), Guid.NewGuid());
            var cart = WhenCommandIsHandled<OkResult<Cart>>(command).Body;
            stdout.WriteLine($"ProductId: {command.ProductId}");
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
        [MemberData(nameof(NumberOfFailures))]
        public void IfProductApiFailsProductShouldBeAddedToCart(int numberOfFailures)
        {
            var productId = Guid.NewGuid();
            AssumeProductApiFailsFor(productId, numberOfFailures);

            var cart = WhenCommandIsHandled<OkResult<Cart>>(new AddItemCommand(Guid.NewGuid(), productId)).Body;

            Assert.Contains(cart.Itens, x => x.ProductId == productId);
        }

        public static TheoryData<int> NumberOfFailures()
        {
            return new TheoryData<int> {1, 2, 3};
        }
        
        [Theory]
        [MemberData(nameof(Products))]
        public void FakeProductTest(WellKnownProduct product, decimal expectedPrice)
        {
            Assert.Equal(expectedPrice, product.Price);
        }

        public static TheoryData<WellKnownProduct, decimal> Products()
        {
            return new TheoryData<WellKnownProduct, decimal>
            {
                {new NikeShoes(), 100} , 
                {new Dummy(), 10}, 
                {new NonExistentProduct(), 0}
            };
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