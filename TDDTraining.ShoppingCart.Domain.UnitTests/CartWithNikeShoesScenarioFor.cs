using TDDTraining.ShoppingCart.Domain.Tests.Shared;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class CartWithNikeShoesScenarioFor<T> where T : WellKnownCustomer, new()
    {
        public Cart Cart { get; }
        
        public CartWithNikeShoesScenarioFor()
        {
            Cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<T>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();
        }
    }
}