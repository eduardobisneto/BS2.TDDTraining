using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Customer
    {
        public Guid Id { get; }
        public CustomerStatus CustomerStatus { get; }

        public Customer(Guid id, CustomerStatus customerStatus)
        {
            Id = id;
            CustomerStatus = customerStatus;
        }
    }
}