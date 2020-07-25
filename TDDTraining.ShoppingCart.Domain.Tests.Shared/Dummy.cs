using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class Dummy : WellKnownProduct
    {
        public override Guid ProductId => Guid.NewGuid();
        public override string Name => nameof(Dummy);
        public override decimal Price => 10;
    }
}