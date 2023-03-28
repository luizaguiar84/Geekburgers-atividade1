using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Models;
using GeekBurger.Products.Repositories;

namespace GeekBurger.Products.Helper
{
    public class MatchStoreFromRepository : IMappingAction<ProductToUpsert, Product>
    {
        private IStoreRepository _storeRepository;
        public MatchStoreFromRepository(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public void Process(ProductToUpsert source, Product destination, ResolutionContext context)
        {
            var store = _storeRepository.GetStoreByName(source.StoreName);

            if (store != null)
                destination.StoreId = store.StoreId;
        }
    }
}