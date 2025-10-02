using DataInsights.API.Models;

namespace DataInsights.API.Repository.Interfaces
{
    public interface ISalesRepository
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<Sale> AddSaleAsync(Sale sale);
        Task<Sale?> UpdateSaleAsync(int id, Sale sale);
        Task<bool> DeleteSaleAsync(int id);
    }
}
