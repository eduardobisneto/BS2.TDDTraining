namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public sealed class PrimeCustomer : WellKnownCustomer
    {
        public override CustomerStatus CustomerStatus => CustomerStatus.Prime;
    }
}