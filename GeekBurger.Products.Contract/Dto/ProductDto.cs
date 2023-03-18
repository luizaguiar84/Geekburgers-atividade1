using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Dto
{
    public class ProductDto
    {
        public Guid StoreId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<ItemDto> Items { get; set; }
        public decimal Price { get; set; }

    }
}
