namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class CustomerBuilder
    {
        private readonly WellKnownCustomer wellKnownCustomer;
        private CustomerBuilder(WellKnownCustomer wellKnownCustomer)
        {
            this.wellKnownCustomer = wellKnownCustomer;
        }

        public static CustomerBuilder For<T>() where T : WellKnownCustomer, new()
        {
            return new CustomerBuilder(new T());
        }

        public Customer Build()
        {
            return new Customer(wellKnownCustomer.CustomerId, wellKnownCustomer.CustomerStatus);
        }
    }
}