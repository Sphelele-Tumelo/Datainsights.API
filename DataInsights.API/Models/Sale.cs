namespace DataInsights.API.Models
{
    public class Sale
    {
        public int Sales_ID { get; set; }

        public int Quantity { get; set; }

        public int Customer_ID { get; set; }

        public decimal Total_Amount { get; set; }

        public DateTime SaleDate { get; set; }
        
        public Product? Product { get; set; }
        public CustomerDTO? Customer { get; set; }

    }
}
