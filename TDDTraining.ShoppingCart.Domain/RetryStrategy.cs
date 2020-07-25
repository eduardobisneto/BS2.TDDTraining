namespace TDDTraining.ShoppingCart.Domain
{
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