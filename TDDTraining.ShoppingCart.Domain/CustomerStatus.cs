namespace TDDTraining.ShoppingCart.Domain
{
    public class CustomerStatus
    {
        public static CustomerStatus Standard { get; }
        public static CustomerStatus Prime { get; }

        static CustomerStatus()
        {
            Standard = new CustomerStatus(0);
            Prime = new CustomerStatus(0.1m);
        }

        private decimal DiscountPercentage { get; }
        
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