using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Cart
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        
        private List<Item> itens;
        public IReadOnlyCollection<Item> Itens => itens.AsReadOnly();

        private Cart()
        {
            Id = Guid.NewGuid();
        }
        
        public Cart(Guid customerId) : this()
        {
            CustomerId = customerId;
            itens = new List<Item>();
        }

        public void AddItem(Guid productId, string productName, decimal productPrice)
        {
            var item = itens.SingleOrDefault(x => x.ProductId == productId);
            if(item == null)
                itens.Add(new Item(productId, productName, productPrice));
            else
                item.IncreaseQuantity();
        }

        public void RemoveItem(Guid productId)
        {
            itens.Remove(itens.Find(x => x.ProductId == productId));
        }
    }
}