using TDDTraining.ShoppingCart.Domain.Commands;
using TDDTraining.ShoppingCart.Domain.Core;
using TDDTraining.ShoppingCart.Domain.Repositories;

namespace TDDTraining.ShoppingCart.Domain.CommandHandlers
{
    public class RemoveItemCommandHandler : IHandleCommand<RemoveItemCommand>
    {
        private readonly ICartRepository repository;

        public RemoveItemCommandHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public IDomainResult Handle(RemoveItemCommand command)
        {
            var cart = repository.GetByCustomerId(command.CustomerId);

            if (cart == null)
                cart = new Cart(new Customer(command.CustomerId, CustomerStatus.Standard));
            else
                cart.RemoveItem(command.ProductId);
            
            repository.Save(cart);

            return new OkResult<Cart>(cart);
        }
    }
}