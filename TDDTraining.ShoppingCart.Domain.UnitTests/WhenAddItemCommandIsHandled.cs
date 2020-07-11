using System;
using Xunit;

namespace TDDTraining.ShoppingCart.Domain.UnitTests
{
    public class WhenAddItemCommandIsHandled
    {
        private readonly Cart cart;
        private readonly AddItemCommand command;

        public WhenAddItemCommandIsHandled()
        {
            command = new AddItemCommand(Guid.NewGuid(), Guid.NewGuid());
            var commandHandler = new AddItemCommandHandler();
            cart = commandHandler.Handle(command);
        }
        
        [Fact]
        public void ItemShouldBePresentInTheCart()
        {
            Assert.Contains(cart.Itens, x => x.ProductId == command.ProductId);
        }

        [Fact]
        public void CartShouldMatchCustomerId()
        {
            Assert.Equal(command.CustomerId, cart.CustomerId);
        }
        
        // Manipular o AddItemCommand quando o carrinho já existir
        //    - dica: introduzir repositorio
        //    - dica: pode quebrar os testes existentes
        //    - dica: forçar a cobrir o cenário de quando o carrinho nao existe
        // Manipular o AddItemCommand para um produto existente - Implementar controle de quantidade
        
        // Implementação básica do commando de Remover Item do carrinho 
    }
}