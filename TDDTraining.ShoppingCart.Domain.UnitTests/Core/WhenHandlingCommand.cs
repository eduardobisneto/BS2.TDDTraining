using TDDTraining.ShoppingCart.Domain.UnitTests.TestDoubles;

namespace TDDTraining.ShoppingCart.Domain.UnitTests.Core
{
    public abstract class WhenHandlingCommand<TCommand, TCommandHandler, TResult>
        where TCommandHandler : IHandleCommand<TCommand, TResult>
    {
        protected ICartRepository Repository { get; }

        protected WhenHandlingCommand()
        {
            Repository = new FakeCartRepository();
        }

        protected TResult WhenCommandIsHandled(TCommand command)
        {
            return CreateCommandHandler().Handle(command);
        }

        protected abstract TCommandHandler CreateCommandHandler();
    }
}