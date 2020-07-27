using TDDTraining.ShoppingCart.Domain.Core;

namespace TDDTraining.ShoppingCart.Domain
{
    public class ProductDoesNotExist : ErrorResult
    {
        public ProductDoesNotExist() : base("The product you tried to add does not exists.") { }
    }
}