using GeekBurger.Products.Models;

namespace GeekBurger.Products.Repositories;

public interface IStoreRepository
{
    Store GetStoreByName(string storeName);
}