using TDDTraining.ShoppingCart.Domain.UnitTests.TestDoubles;

namespace TDDTraining.ShoppingCart.Domain.UnitTests.Core
{
    public abstract class WhenHandlingCommand<TCommand, TCommandHandler>
        where TCommandHandler : IHandleCommand<TCommand, IDomainResult>
    {
        protected ICartRepository Repository { get; }

        protected WhenHandlingCommand()
        {
            Repository = new FakeCartRepository();
        }

        protected IDomainResult WhenCommandIsHandled(TCommand command)
        {
            return CreateCommandHandler().Handle(command);
        }

        protected abstract TCommandHandler CreateCommandHandler();
    }
}