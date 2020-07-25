using System;
using Polly;

namespace TDDTraining.ShoppingCart.Domain
{
    public class AddItemCommandHandler : IHandleCommand<AddItemCommand, IDomainResult>
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductApi productApi;
        private readonly RetryStrategy retryStrategy;

        public AddItemCommandHandler(ICartRepository cartRepository, IProductApi productApi, RetryStrategy retryStrategy)
        {
            this.cartRepository = cartRepository;
            this.productApi = productApi;
            this.retryStrategy = retryStrategy;
        }
        
        public IDomainResult Handle(AddItemCommand command)
        {
            var cart = 
                cartRepository.GetByCustomerId(command.CustomerId) 
                ?? new Cart(command.CustomerId);

            var productApiRetry = Policy
                .Handle<Exception>()
                .WaitAndRetry(retryStrategy.RetryCount, 
                              (attempt) => TimeSpan.FromMilliseconds(retryStrategy.Milliseconds));
            
            var productInfo = productApiRetry.Execute(() => productApi.GetProduct(command.ProductId));

            if(productInfo == null)
                return new ErrorResult("Try again later. Some of our services are unavailable.");
            
            cart.AddItem(command.ProductId, productInfo.ProductName, productInfo.Price);

            cartRepository.Save(cart);
            
            return new OkResult<Cart>(cart);
        }
    }

    public interface IDomainResult
    {
    }

    public class OkResult<T> : IDomainResult
    {
        public T Body { get; }

        public OkResult(T body)
        {
            Body = body;
        }
    }

    public class ErrorResult : IDomainResult
    {
        public string Message { get; }

        public ErrorResult(string message)
        {
            Message = message;
        }
    }
}