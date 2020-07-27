namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class Dummy : WellKnowProduct
    {
        public override string Name => nameof(Dummy);
        public override decimal Price => 10;
    }
}