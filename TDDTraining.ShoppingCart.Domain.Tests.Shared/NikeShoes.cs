namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class NikeShoes : WellKnownProduct
    {
        public override string Name => nameof(NikeShoes);
        public override decimal Price => 100;
    }

    public class NonExistentProduct : WellKnownProduct
    {
        public override string Name => nameof(NonExistentProduct);
        public override decimal Price => 0;
    }
}