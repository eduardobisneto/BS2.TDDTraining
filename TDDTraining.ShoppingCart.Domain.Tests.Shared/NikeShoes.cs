namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class NikeShoes : WellKnowProduct
    {
        public override string Name => nameof(NikeShoes);
        public override decimal Price => 100;
    }

    public class NonExistentProduct : WellKnowProduct
    {
        public override string Name => nameof(NonExistentProduct);
        public override decimal Price => 0;
    }
}