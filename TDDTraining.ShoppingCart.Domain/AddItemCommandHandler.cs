namespace TDDTraining.ShoppingCart.Domain
{
    public class AddItemCommandHandler : IHandleCommand<AddItemCommand, Cart>
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductApi productApi;

        public AddItemCommandHandler(ICartRepository cartRepository, IProductApi productApi)
        {
            this.cartRepository = cartRepository;
            this.productApi = productApi;
        }
        
        public Cart Handle(AddItemCommand command)
        {
            var cart = 
                cartRepository.GetByCustomerId(command.CustomerId) 
                ?? new Cart(command.CustomerId);

            var productInfo = productApi.GetProduct(command.ProductId);
            cart.AddItem(command.ProductId, productInfo.Price);

            cartRepository.Save(cart);
            
            return cart;
        }
    }
}