namespace DataInsights.API.DTOs
{
    public class MonthlySalesReportDTO
    {
        public string? Month { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }

        public List<ProductSalesDTO>? TopProducts { get; set; }
    }
}
