using Microsoft.EntityFrameworkCore.ChangeTracking;
using GeekBurger.Products.Models;

namespace GeekBurger.Products.Service
{
    public interface IProductChangedService : IHostedService
    {
        void SendMessagesAsync();
        void AddToMessageList(IEnumerable<EntityEntry<Product>> changes);
    }
}