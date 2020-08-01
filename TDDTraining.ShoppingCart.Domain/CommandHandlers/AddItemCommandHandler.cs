using System;
using Polly;
using TDDTraining.ShoppingCart.Domain.Apis;
using TDDTraining.ShoppingCart.Domain.Commands;
using TDDTraining.ShoppingCart.Domain.Core;
using TDDTraining.ShoppingCart.Domain.Repositories;

namespace TDDTraining.ShoppingCart.Domain.CommandHandlers
{
    public class AddItemCommandHandler : IHandleCommand<AddItemCommand>
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductApi productApi;
        private readonly RetryStrategy retryStrategy;
        private readonly ILogger logger;

        public AddItemCommandHandler(ICartRepository cartRepository, IProductApi productApi, RetryStrategy retryStrategy, ILogger logger)
        {
            this.cartRepository = cartRepository;
            this.productApi = productApi;
            this.retryStrategy = retryStrategy;
            this.logger = logger;
        }
        
        public IDomainResult Handle(AddItemCommand command)
        {
            var cart = cartRepository.GetByCustomerId(command.CustomerId) 
                       ?? new Cart(new Customer(command.CustomerId, CustomerStatus.Standard));

            var productApiRetry = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryStrategy.RetryCount, 
                    attempt => TimeSpan.FromMilliseconds(retryStrategy.WaitMilliseconds));
            
            Apis.ProductInfo productInfo = null;
            
            try
            {
                productInfo = productApiRetry.Execute(() => productApi.GetProduct(command.ProductId));
            }
            catch (Exception e)
            {
                logger.LogError(e);
                return new ServiceUnavailableError();
            }

            if (productInfo == null)
            {
                return new ProductDoesNotExistError();
            }

            cart.AddItem(command.ProductId, productInfo.ProductName, productInfo.Price);

            cartRepository.Save(cart);
            
            return new OkResult<Cart>(cart);
        }
    }
}