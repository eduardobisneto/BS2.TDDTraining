using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public abstract class WellKnownProduct
    {
        public abstract Guid ProductId { get; }
        public abstract string Name { get; }
        public abstract decimal Price { get; }
    }
}