namespace TDDTraining.ShoppingCart.Domain
{
    public class RetryStrategy
    {
        public int RetryCount { get; }
        public int WaitMilliseconds { get; }

        private RetryStrategy(int retryCount, int waitMilliseconds)
        {
            RetryCount = retryCount;
            WaitMilliseconds = waitMilliseconds;
        }
        
        public static RetryStrategy CreateAddItemCommandRetryStrategy() => new RetryStrategy(3, 50);
    }
}