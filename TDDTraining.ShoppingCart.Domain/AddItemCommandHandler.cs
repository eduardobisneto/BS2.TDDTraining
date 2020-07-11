using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class AddItemCommandHandler
    {
        public Cart Handle(AddItemCommand command)
        {
            var cart = new Cart(command.CustomerId);
            cart.AddItem(command.ProductId);
            return cart;
        }
    }
}