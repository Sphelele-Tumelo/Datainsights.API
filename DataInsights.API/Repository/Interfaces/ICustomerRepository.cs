using DataInsights.API.Models;

namespace DataInsights.API.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int id);
        Task<CustomerDTO> AddCustomerAsync(CustomerDTO customer);

        Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO customer);

        Task<bool> DeleteCustomerAsync(int id);
    }
}
