namespace TDDTraining.ShoppingCart.Domain
{
    public class RemoveItemCommandHandler : IHandleCommand<RemoveItemCommand, IDomainResult>
    {
        private readonly ICartRepository repository;

        public RemoveItemCommandHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public IDomainResult Handle(RemoveItemCommand command)
        {
            var cart = repository.GetByCustomerId(command.CustomerId);
            
            if(cart == null)
                return new OkResult<Cart>(new Cart(command.CustomerId));
            
            cart.RemoveItem(command.ProductId);
            return new OkResult<Cart>(cart);
        }
    }
}