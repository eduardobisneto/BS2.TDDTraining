using TDDTraining.ShoppingCart.Domain.Core;

namespace TDDTraining.ShoppingCart.Domain
{
    public class ProductDoesNotExistError : ErrorResult
    {
        public ProductDoesNotExistError() : base("The product you tried to add does not exists.") { }
    }
}