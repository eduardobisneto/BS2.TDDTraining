using System;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public abstract class WhenCalculatingCartTotal<T> : IClassFixture<CartWithNikeShoesScenarioFor<T>> where T : WellKnownCustomer, new()
    {
        protected CartWithNikeShoesScenarioFor<T> Scenario { get; }

        protected WhenCalculatingCartTotal(CartWithNikeShoesScenarioFor<T> scenario)
        {
            Scenario = scenario;
        }
        
        [Fact]
        public void ItemsTotalShouldBeSumOfItemsTimesQuantity()
        {
            Assert.Equal(100, Scenario.Cart.ItemsTotal);
        }
    }
}