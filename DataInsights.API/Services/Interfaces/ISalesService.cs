using DataInsights.API.DTOs;

namespace DataInsights.API.Services.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<Sale_DTO>> GetAllSalesAsync();
        Task<Sale_DTO?> GetSaleByIdAsync(int id);
        Task<Sale_DTO> AddSaleAsync(Sale_DTO sale);
        Task<Sale_DTO?> UpdateSaleAsync(int id, Sale_DTO sale);

        Task<bool> DeleteSaleAsync(int id);
    }
}
