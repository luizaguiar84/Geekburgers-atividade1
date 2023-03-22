using GeekBurger.Products.Contract.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Repositories
{
    public interface IProductsRepository
    {
        bool Add(Product product);
        Task<List<Product>> GetFullListOfItems();
        IEnumerable<Product> GetProductsByStoreName(string storeName);
        void Save();
        Product GetProductById(Guid id);
    }

    public class ProductsRepository : IProductsRepository
    {
        private ProductDbContext _context;

        public ProductsRepository(ProductDbContext context)
        {
            _context = context;            
            _context.Seed();
        }

        public bool Add(Product product)
        {
            product.Id = Guid.NewGuid();
            _context.Products.Add(product);
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetProductsByStoreName(string storeName)
        {
            var products = _context.Products?
            .Where(product =>
                product.Store.Name.Equals(storeName,
                StringComparison.InvariantCultureIgnoreCase))
            .Include(product => product.Items);

            return products;
        }

        public async Task<List<Product>> GetFullListOfItems()
        {
            return await _context.Products.ToListAsync();
        }

        public Product GetProductById(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }
    }

}
