namespace Blazor_Game_Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }

        public Product(int id, string name, decimal price, ProductCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }

    public enum ProductCategory
    {
        Weapon,
        Armour,
        Consumable
    }
}
