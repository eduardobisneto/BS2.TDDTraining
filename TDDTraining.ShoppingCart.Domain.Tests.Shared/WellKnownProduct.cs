using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public abstract class WellKnownProduct
    {
        private Guid? productId;
        public virtual Guid ProductId => productId ?? (productId = Guid.NewGuid()).Value;
        public abstract string Name { get; }
        public abstract decimal Price { get; }
    }
}