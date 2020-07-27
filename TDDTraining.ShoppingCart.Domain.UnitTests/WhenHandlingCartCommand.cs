using System;
using Moq;
using TDDTraining.ShoppingCart.Domain.Apis;
using TDDTraining.ShoppingCart.Domain.CommandHandlers;
using TDDTraining.ShoppingCart.Domain.Commands;
using TDDTraining.ShoppingCart.Domain.Core;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using TDDTraining.ShoppingCart.Domain.UnitTests.Core;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public abstract class WhenHandlingCartCommand<TCommand, TCommandHandler> : WhenHandlingCommand<TCommand, TCommandHandler>
        where TCommandHandler : IHandleCommand<TCommand>
    {
        
        protected Cart AssumeCartAlreadyExists(Guid customerId)
        {
            var result = new AddItemCommandHandler(
                    Repository, 
                    CreateProductApiStub().Object, 
                    RetryStrategy.CreateAddItemCommandRetryStrategy(),
                    CreateLoggerMock().Object)
                .Handle(new AddItemCommand(customerId, Guid.NewGuid()));

            return ((OkResult<Cart>) result).Body;
        }

        protected static Mock<IProductApi> CreateProductApiStub()
        {
            var productApiStub = new Mock<IProductApi>();
            productApiStub.Setup(x => x.GetProduct(It.IsAny<Guid>()))
                .Returns(ProductInfoBuilder.For<Dummy>().Build());

            return productApiStub;
        }

        protected void GivenProductAlreadyExistsInCart(Guid productId, Guid customerId)
        {
            new AddItemCommandHandler(Repository, 
                    CreateProductApiStub().Object, 
                    RetryStrategy.CreateAddItemCommandRetryStrategy(), 
                    CreateLoggerMock().Object)
                .Handle(new AddItemCommand(customerId, productId));
        }

        protected static Mock<ILogger> CreateLoggerMock()
        {
            return new Mock<ILogger>();
        }
        
    }
}