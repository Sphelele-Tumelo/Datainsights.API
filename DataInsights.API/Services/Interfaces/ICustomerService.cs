using DataInsights.API.DTOs;

namespace DataInsights.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer_DTO>> GetAllCustomersAsync();
        Task<Customer_DTO?> GetCustomerByIdAsync(int id);
        Task<Customer_DTO> AddCustomerAsync(Customer_DTO customer);
        Task<Customer_DTO?> UpdateCustomerAsync(int id, Customer_DTO customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
