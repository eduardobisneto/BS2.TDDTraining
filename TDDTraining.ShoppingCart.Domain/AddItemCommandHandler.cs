using System;
using Polly;

namespace TDDTraining.ShoppingCart.Domain
{
    public class AddItemCommandHandler : IHandleCommand<AddItemCommand, Cart>
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
        
        public Cart Handle(AddItemCommand command)
        {
            var cart = 
                cartRepository.GetByCustomerId(command.CustomerId) 
                ?? new Cart(command.CustomerId);

            var productApiRetry = Policy
                .Handle<Exception>()
                .WaitAndRetry(retryStrategy.RetryCount, 
                              (attempt) => TimeSpan.FromMilliseconds(retryStrategy.Milliseconds));
            
            var productInfo = productApiRetry.Execute(() => productApi.GetProduct(command.ProductId));

            cart.AddItem(command.ProductId, productInfo.ProductName, productInfo.Price);

            cartRepository.Save(cart);
            
            return cart;
        }
    }

    public class RetryStrategy
    {
        public int RetryCount { get; }
        public int Milliseconds { get; }

        private RetryStrategy(int retryCount, int milliseconds)
        {
            RetryCount = retryCount;
            Milliseconds = milliseconds;
        }
        
        public static RetryStrategy CreateRetryStrategy() => new RetryStrategy(3, 50);
    }
}