using System;
using Moq;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using TDDTraining.ShoppingCart.Domain.UnitTests.Core;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public abstract class WhenHandlingCartCommand<TCommand, TCommandHandler> : WhenHandlingCommand<TCommand, TCommandHandler>
        where TCommandHandler : IHandleCommand<TCommand, IDomainResult>
    {
        protected void GivenProductAlreadyExistsInCart(Guid productId, Guid customerId)
        {
            new AddItemCommandHandler(Repository, CreateProductApiStub().Object, RetryStrategy.CreateRetryStrategy()).Handle(new AddItemCommand(customerId, productId));
        }

        protected static Mock<IProductApi> CreateProductApiStub()
        {
            var productApiStub = new Mock<IProductApi>();

            productApiStub
                .Setup(x => x.GetProduct(It.IsAny<Guid>()))
                .Returns(ProductInfoBuilder.For<Dummy>().Build());
            
            return productApiStub;
        }

        protected Cart AssumeCartAlreadyExists(Guid customerId)
        {
            return ((OkResult<Cart>)new AddItemCommandHandler(Repository, CreateProductApiStub().Object, RetryStrategy.CreateRetryStrategy())
                    .Handle(new AddItemCommand(customerId, Guid.NewGuid()))).Body;
        }
    }
}