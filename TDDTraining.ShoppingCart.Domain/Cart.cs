using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDTraining.ShoppingCart.Domain
{
    public class Cart
    {
        public Customer Customer { get; }
        public Guid Id { get; }
        
        private List<Item> itens;
        public IReadOnlyCollection<Item> Itens => itens.AsReadOnly();
        public decimal Total => ItemsTotal - Discount;
        public decimal Discount => Customer.CustomerStatus.GetDiscount(ItemsTotal);
        public decimal ItemsTotal => itens.Sum(x => x.Total);

        private Cart()
        {
            Id = Guid.NewGuid();
        }
        
        public Cart(Customer customer) : this()
        {
            Customer = customer;
            itens = new List<Item>();
        }

        public void AddItem(Guid productId, string productName, decimal productPrice)
        {
            var item = itens.SingleOrDefault(x => x.ProductId == productId);

            if (item == null)
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