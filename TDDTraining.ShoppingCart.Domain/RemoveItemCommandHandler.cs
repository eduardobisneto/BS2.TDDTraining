namespace TDDTraining.ShoppingCart.Domain
{
    public class RemoveItemCommandHandler : IHandleCommand<RemoveItemCommand, Cart>
    {
        private readonly ICartRepository repository;

        public RemoveItemCommandHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public Cart Handle(RemoveItemCommand command)
        {
            var cart = repository.GetByCustomerId(command.CustomerId);
            
            if(cart == null)
                return new Cart(command.CustomerId);
            
            cart.RemoveItem(command.ProductId);
            return cart;
        }
    }
}