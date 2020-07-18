namespace TDDTraining.ShoppingCart.Domain
{
    public interface IHandleCommand<TCommand, TResult>
    {
        TResult Handle(TCommand command);
    }
}