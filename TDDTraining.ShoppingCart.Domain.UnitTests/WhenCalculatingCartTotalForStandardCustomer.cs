using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenCalculatingCartTotalForStandardCustomer : WhenCalculatingCartTotal<StandardCustomer>
    {
        public WhenCalculatingCartTotalForStandardCustomer(CartWithNikeShoesScenarioFor<StandardCustomer> scenario) : base(scenario) { }
        
        [Fact]
        public void CartTotalShouldBeItemsTotalMinusDiscount()
        {
            Assert.Equal(100, Scenario.Cart.Total);
        }
        
        [Fact]
        public void DiscountShouldBeZero()
        {
            Assert.Equal(0, Scenario.Cart.Discount);
        }
    }
}