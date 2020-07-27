namespace TDDTraining.ShoppingCart.Domain.Core
{
    public interface IHandleCommand<TCommand>
    {
        IDomainResult Handle(TCommand command);
    }
}