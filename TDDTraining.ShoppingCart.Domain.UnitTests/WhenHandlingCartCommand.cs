using System;
using Moq;
using TDDTraining.ShoppingCart.Domain.UnitTests.Core;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public abstract class WhenHandlingCartCommand<TCommand, TCommandHandler, TResult> : WhenHandlingCommand<TCommand, TCommandHandler, TResult>
        where TCommandHandler : IHandleCommand<TCommand, TResult>
    {
        protected void GivenProductAlreadyExistsInCart(Guid productId, Guid customerId)
        {
            new AddItemCommandHandler(Repository, CreateProductApiStub().Object).Handle(new AddItemCommand(customerId, productId));
        }

        protected static Mock<IProductApi> CreateProductApiStub()
        {
            var productApiStub = new Mock<IProductApi>();

            productApiStub
                .Setup(x => x.GetProduct(It.IsAny<Guid>()))
                .Returns(new ProductInfo(Guid.NewGuid(), 10));
            
            return productApiStub;
        }

        protected Cart AssumeCartAlreadyExists(Guid customerId)
        {
            return new AddItemCommandHandler(Repository, CreateProductApiStub().Object).Handle(new AddItemCommand(customerId, Guid.NewGuid()));
        }
    }
}