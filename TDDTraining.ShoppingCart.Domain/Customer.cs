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

    public class CustomerStatus
    {
        public static CustomerStatus Standard { get; }
        public static CustomerStatus Prime { get; }

        static CustomerStatus()
        {
            Standard = new CustomerStatus(0);
            Prime = new CustomerStatus(0.1m);
        }
        
        public decimal DiscountPercentage { get; }
        
        private CustomerStatus(decimal discountPercentage)
        {
            DiscountPercentage = discountPercentage;
        }

        public decimal GetDiscount(in decimal itemsTotal)
        {
            return itemsTotal * DiscountPercentage;
        }
    }
}