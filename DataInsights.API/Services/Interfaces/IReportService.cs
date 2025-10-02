using DataInsights.API.DTOs;

namespace DataInsights.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<MonthlySalesReportDTO> GetMonthlySales(int year, int month);
    }
}
