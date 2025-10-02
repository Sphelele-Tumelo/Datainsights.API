namespace DataInsights.API.Models
{
    public class CustomerDTO
    {
        public int _customerID { get; set; }

        public string? _customerName { get; set; }

        public string? _customerEmail { get; set; }

        public ICollection<Sale>? _sales { get; set; }
    }
}
