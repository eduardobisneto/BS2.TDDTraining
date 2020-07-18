using System;

namespace TDDTraining.ShoppingCart.Domain
{
    public class AddItemCommandHandler
    {
        private readonly ICartRepository cartRepository;

        public AddItemCommandHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        
        public Cart Handle(AddItemCommand command)
        {
            var cart = 
                cartRepository.GetByCustomerId(command.CustomerId) 
                ?? new Cart(command.CustomerId);

            cart.AddItem(command.ProductId);

            cartRepository.Save(cart);
            
            return cart;
        }
    }

    public interface ICartRepository
    {
        Cart GetByCustomerId(Guid customerId);
        void Save(Cart cart);
    }
}