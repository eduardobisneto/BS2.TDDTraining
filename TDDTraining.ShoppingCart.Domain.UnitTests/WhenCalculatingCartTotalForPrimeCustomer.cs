using System;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenCalculatingCartTotalForPrimeCustomer : WhenCalculatingCartTotal<PrimeCustomer>
    {
        public WhenCalculatingCartTotalForPrimeCustomer(CartWithNikeShoesScenarioFor<PrimeCustomer> scenario) : base(scenario) { }
        
        [Fact]
        public void CartTotalShouldBeItemsTotalMinusDiscount()
        {
            Assert.Equal(90, Scenario.Cart.Total);
        }
        
        [Fact]
        public void DiscountShouldBeTeenPercent()
        {
            Assert.Equal(10, Scenario.Cart.Discount);
        }
    }

    public class SharedScenario
    {
        public SharedScenario()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }

    [CollectionDefinition(nameof(SharedScenarioCollectionDefinition))]
    public class SharedScenarioCollectionDefinition : ICollectionFixture<SharedScenario> { }
    
    [Collection(nameof(SharedScenarioCollectionDefinition))]
    public class WhenSomething
    {
        private readonly SharedScenario scenario;

        public WhenSomething(SharedScenario scenario)
        {
            this.scenario = scenario;
        }

        [Fact]
        public void TestSomething()
        {
            Assert.NotNull(scenario.Id);
        }
    }
    
    [Collection(nameof(SharedScenarioCollectionDefinition))]
    public class WhenSomeOtherThing
    {
        private readonly SharedScenario scenario;

        public WhenSomeOtherThing(SharedScenario scenario)
        {
            this.scenario = scenario;
        }

        [Fact]
        public void TestingSomeOtherThing()
        {
            Assert.NotNull(scenario.Id);
        }
    
    }
}