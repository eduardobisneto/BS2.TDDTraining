using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class Dummy : WellKnownProduct
    {
        public override Guid ProductId => new Guid("333380e7-95c4-4eec-a95a-9635acccdfff");
        public override string Name => nameof(Dummy);
        public override decimal Price => 10;
    }
}