using AutoMapper;
using GeekBurger.Products.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.MapperProfiles
{

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Item, ItemDto>();
            CreateMap<ProductToUpsert, Product>()
    .AfterMap<MatchStoreFromRepository>();
            CreateMap<ItemToUpsert, Item>()
                .AfterMap<MatchItemsFromRepository>();

        }
    }
}


