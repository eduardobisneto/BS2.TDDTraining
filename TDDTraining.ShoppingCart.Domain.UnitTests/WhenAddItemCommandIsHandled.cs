using System;
using System.Linq;
using Moq;
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

    public class ProductInfoBuilder
    {
        private readonly WellKnownProduct wellKnowProduct;

        private ProductInfoBuilder(WellKnownProduct wellKnowProduct)
        {
            this.wellKnowProduct = wellKnowProduct;
        }

        public static ProductInfoBuilder For<T>() where T : WellKnownProduct, new()
        {
            var wellKnowProduct = new T();
            return new ProductInfoBuilder(wellKnowProduct);
        }

        public ProductInfo Build()
        {
            return new ProductInfo(wellKnowProduct.ProductId, wellKnowProduct.Name, wellKnowProduct.Price);
        }
    }

    public abstract class WellKnownProduct
    {
        public abstract Guid ProductId { get; }
        public abstract string Name { get; }
        public abstract decimal Price { get; }
    }

    public class NikeShoes : WellKnownProduct
    {
        public override Guid ProductId => Guid.NewGuid();
        public override string Name => nameof(NikeShoes);
        public override decimal Price => 100;
    }
    
    public class Dummy : WellKnownProduct
    {
        public override Guid ProductId => Guid.NewGuid();
        public override string Name => nameof(Dummy);
        public override decimal Price => 10;
    }
}