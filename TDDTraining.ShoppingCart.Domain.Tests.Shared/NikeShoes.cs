using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class NikeShoes : WellKnownProduct
    {
        public override Guid ProductId => new Guid("617380e7-95c4-4eec-a95a-9635acccdfd5");
        public override string Name => nameof(NikeShoes);
        public override decimal Price => 100;
    }
}