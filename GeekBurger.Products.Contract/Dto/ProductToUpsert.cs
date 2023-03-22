﻿namespace GeekBurger.Products.Contract.Dto
{
    public class ProductToUpsert
    {
        public string StoreName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<ItemToUpsert> Items { get; set; }
    }
    public class ItemToUpsert
    {
        public string Name { get; set; }
    }

}
