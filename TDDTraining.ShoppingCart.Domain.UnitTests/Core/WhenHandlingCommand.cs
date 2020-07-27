using TDDTraining.ShoppingCart.Domain.Core;
using TDDTraining.ShoppingCart.Domain.Repositories;
using TDDTraining.ShoppingCart.Domain.UnitTests.TestDoubles;

namespace TDDTraining.ShoppingCart.Domain.UnitTests.Core
{
    public abstract class WhenHandlingCommand<TCommand, TCommandHandler>
        where TCommandHandler : IHandleCommand<TCommand>
    {
        protected ICartRepository Repository { get; }
        
        protected WhenHandlingCommand()
        {
            Repository = new FakeCartRepository(); 
        }

        protected TResult WhenCommandIsHandled<TResult>(TCommand command) where TResult : class, IDomainResult
        {
            return CreateCommandHandler().Handle(command) as TResult;
        }

        protected abstract TCommandHandler CreateCommandHandler();

    }
}