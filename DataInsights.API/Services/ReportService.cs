using DataInsights.API.Data;
using DataInsights.API.DTOs;
using DataInsights.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataInsights.API.Services
{
    public class ReportService : IReportService
    {
        private readonly SQLDatabase _context;
        private readonly PostgreSQLDb _postgreSQL;

        public ReportService(SQLDatabase context, PostgreSQLDb postgreSQL)
        {
            _context = context;
            _postgreSQL = postgreSQL;
        }

        public async Task<MonthlySalesReportDTO> GetMonthlySales(int month, int year)
        {
            // for SQL server 
            var sales = await _context.Sales
                .Include(s => s.Product)
                .Where(s => s.SaleDate.Year == year && s.SaleDate.Month == month)
                .ToListAsync();

            // for PostgreSQL
            var salesPg = await _postgreSQL.Sales
               .Include(s => s.Product)
               .Where(s => s.SaleDate.Year == year && s.SaleDate.Month == month)
               .ToListAsync();


            var monthlyReport = new MonthlySalesReportDTO
            {
                Month = new DateTime (year, month, 1).ToString("MMMM yyyy"),
                TotalSales = sales.Sum(s => s.Total_Amount) + salesPg.Sum(p => p.Total_Amount),
                TotalOrders = sales.Count() + salesPg.Count(),
                TopProducts =sales
                    .Concat(salesPg)
                    .GroupBy(s => s.Product?.Product_Name)
                    .Select(g => new ProductSalesDTO
                    {
                        ProductName = g.Key,
                        QuantitySold = g.Sum(s => s.Quantity),
                        Revenue = g.Sum(s => s.Total_Amount)
                    })
                    .OrderByDescending(p => p.Revenue)
                    .Take(5)
                    .ToList()

            };

            return monthlyReport;
        }
    }
}
