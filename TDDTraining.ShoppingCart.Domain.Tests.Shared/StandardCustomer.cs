namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public sealed class StandardCustomer : WellKnownCustomer
    {
        public override CustomerStatus CustomerStatus => CustomerStatus.Standard;
    }
}