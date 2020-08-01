using System;
using TDDTraining.ShoppingCart.Domain.Tests.Shared;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenCalculatingCartTotalForStandardCustomer
    {
        [Fact]
        public void CartTotalShouldBeItemsTotalMinusDiscount()
        {
            var cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<StandardCustomer>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();

            Assert.Equal(100, cart.Total);
        }
        
        [Fact]
        public void DiscountShouldBeZero()
        {            
            var cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<StandardCustomer>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();
            
            Assert.Equal(0, cart.Discount);
        }
        
        [Fact]
        public void ItemsTotalShouldBeSumOfItemsTimesQuantity()
        {
            var cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<StandardCustomer>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();
            
            Assert.Equal(100, cart.ItemsTotal);
        }
    }
    
    public class WhenCalculatingCartTotalForPrimeCustomer
    {
        [Fact]
        public void CartTotalShouldBeItemsTotalMinusDiscount()
        {
            var cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<PrimeCustomer>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();
            
            Assert.Equal(90, cart.Total);
        }
        
        [Fact]
        public void DiscountShouldBeTeenPercent()
        {
            var cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<PrimeCustomer>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();
            
            Assert.Equal(10, cart.Discount);
        }
        
        [Fact]
        public void ItemsTotalShouldBeSumOfItemsTimesQuantity()
        {
            var cart = new CartBuilder()
                .WithCustomer(CustomerBuilder.For<PrimeCustomer>())
                .WithItem(ItemBuilder.For<NikeShoes>())
                .Build();
            
            Assert.Equal(100, cart.ItemsTotal);
        }
    }
}