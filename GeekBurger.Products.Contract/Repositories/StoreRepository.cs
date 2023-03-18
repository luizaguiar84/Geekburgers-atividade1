using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StoreRepository : IStoreRepository
{
    private ProductDbContext _context { get; set; }

    public StoreRepository(ProductDbContext context)
    {
        _context = context;
    }

    public Store GetStoreByName(string storeName)
    {
        return _context.Stores.FirstOrDefault(store => store.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase));
    }
}

public interface IStoreRepository
{
    Store GetStoreByName(string storeName);
}