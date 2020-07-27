namespace TDDTraining.ShoppingCart.Domain.Core
{
    public class OkResult<T> : IDomainResult
    {
        public T Body { get; }

        public OkResult(T body)
        {
            Body = body;
        }
    }
}