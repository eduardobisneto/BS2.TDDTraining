namespace TDDTraining.ShoppingCart.Domain.Core
{
    public abstract class ErrorResult : IDomainResult
    {
        public string Message { get; }

        protected ErrorResult(string message)
        {
            Message = message;
        }
    }
}