using System;
using System.Collections.Generic;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Cart
    {
        private List<Item> itens;
        public IReadOnlyCollection<Item> Itens => itens.AsReadOnly();
        public Guid CustomerId { get; }

        public Cart(Guid customerId)
        {
            CustomerId = customerId;
            itens = new List<Item>();
        }

        public void AddItem(Guid productId)
        {
            itens.Add(new Item(productId));
        }
    }
}