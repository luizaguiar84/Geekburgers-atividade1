namespace GeekBurger.Products.Contract;

public class Product
{
    public Guid Id { get; set; }
    public Store Store { get; set; }
    public Guid StoreId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public List<Item> Items { get; set; }
    public decimal Price { get; set; }
}
