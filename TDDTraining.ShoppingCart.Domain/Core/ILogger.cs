using System;
using System.Threading.Tasks;

namespace TDDTraining.ShoppingCart.Domain.Core
{
    public interface ILogger
    {
        Task LogError(Exception exception);
    }
}