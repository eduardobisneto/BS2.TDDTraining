using System;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public abstract class WellKnownCustomer
    {
        private Guid? customerId;
        public virtual Guid CustomerId => customerId ?? (customerId = Guid.NewGuid()).Value;
        
        public abstract CustomerStatus CustomerStatus { get; }
    }
}