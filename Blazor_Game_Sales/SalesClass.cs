using Blazor_Game_Product;

namespace Blazor_Game_Sales
{
    public class Sales
    {
        public List<Sale> SaleList { get; set; }
        public Sales()
        {
            SaleList = new List<Sale>();
        }
        public void AddSale(Sale sale)
        {
            SaleList.Add(sale);
        }
    }

    public class Sale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public List<Product> WeaponList = new List<Product>();
        public List<Product> ArmourList = new List<Product>();
        public List<Product> ConsumableList = new List<Product>();

        public Sale(int id, string name, decimal price, ProductCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
