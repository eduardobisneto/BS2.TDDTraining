using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class NikeShoes : WellKnownProduct
    {
        public override Guid ProductId => Guid.NewGuid();
        public override string Name => nameof(NikeShoes);
        public override decimal Price => 100;
    }
}