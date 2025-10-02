namespace DataInsights.API.Models
{
    public class Product
    {
        public int Product_ID { get; set; }
        public string? Product_Name { get; set; }
        public string ? Product_Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ICollection<Sale>? Sales { get; set; }
    }
}
