using AutoMapper;
using GeekBurger.Products.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Dto
{
    public class MatchStoreFromRepository :
     IMappingAction<ProductToUpsert, Product>
    {
        private IStoreRepository _storeRepository;
        public MatchStoreFromRepository(IStoreRepository
            storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public void Process(ProductToUpsert source,Product destination, ResolutionContext context)
        {
            var store =
                _storeRepository.GetStoreByName(source.StoreName);

            if (store != null)
                destination.StoreId = store.Id;
        }
        
    }

    public class MatchItemsFromRepository :
    IMappingAction<ItemToUpsert, Item>
    {
        private IProductsRepository _productRepository;
        public MatchItemsFromRepository(IProductsRepository
            productRepository)
        {
            _productRepository = productRepository;
        }

        public void Process(ItemToUpsert source, Item destination)
        {
            var fullListOfItems =
                _productRepository.GetFullListOfItems();

            var itemFound = fullListOfItems?
                .FirstOrDefault(item => item.Name
                .Equals(source.Name,
                    StringComparison.InvariantCultureIgnoreCase));

            if (itemFound != null)
                destination.Id = itemFound.ItemId;
            else
                destination.Id = Guid.NewGuid();
        }

        public void Process(ItemToUpsert source, Item destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }

}
