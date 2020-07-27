using TDDTraining.ShoppingCart.Domain.Core;

namespace TDDTraining.ShoppingCart.Domain
{
    public class ServiceUnavailableError : ErrorResult
    {
        public ServiceUnavailableError() : base("Try again later. Some of our services are unavailable.") { }
    }
}