using System;
using System.Collections.Generic;

namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class CartBuilder
    {
        private List<ItemBuilder> itemBuilders = new List<ItemBuilder>();
        private CustomerBuilder customerBuilder;

        public Cart Build()
        {
            var cart = new Cart(customerBuilder.Build());

            foreach (var itemBuilder in itemBuilders)
            {
                var item = itemBuilder.Build();
                cart.AddItem(item.ProductId, item.ProductName, item.ProductPrice);
            }
            
            return cart;
        }

        public CartBuilder WithCustomer(CustomerBuilder customerBuilder)
        {
            this.customerBuilder = customerBuilder;
            return this;
        }

        public CartBuilder WithItem(ItemBuilder itemBuilder)
        {
            itemBuilders.Add(itemBuilder);
            return this;
        }
    }
}