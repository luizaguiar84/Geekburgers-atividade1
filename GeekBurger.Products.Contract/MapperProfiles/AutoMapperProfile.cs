using AutoMapper;
using GeekBurger.Products.Contract.Dto;

namespace GeekBurger.Products.Contract.MapperProfiles
{

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Item, ItemDto>();

            CreateMap<ProductToUpsert, Product>();
            //.AfterMap<MatchStoreFromRepository>();

            CreateMap<ItemToUpsert, Item>();
                //.AfterMap<MatchItemsFromRepository>();

        }
    }
}


