using GeekBurger.Products.Models;

namespace GeekBurger.Products.Repositories;

public interface IProductChangedEventRepository
{
    ProductChangedEvent Get(Guid eventId);
    bool Add(ProductChangedEvent productChangedEvent);
    bool Update(ProductChangedEvent productChangedEvent);
    void Save();
}