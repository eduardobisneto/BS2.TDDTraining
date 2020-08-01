using System;

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

    public abstract class WellKnownCustomer
    {
        private Guid? customerId;
        public virtual Guid CustomerId => customerId ?? (customerId = Guid.NewGuid()).Value;
        
        public abstract CustomerStatus CustomerStatus { get; }
    }
    
    public sealed class PrimeCustomer : WellKnownCustomer
    {
        public override CustomerStatus CustomerStatus => CustomerStatus.Prime;
    }

    public sealed class StandardCustomer : WellKnownCustomer
    {
        public override CustomerStatus CustomerStatus => CustomerStatus.Standard;
    }
}