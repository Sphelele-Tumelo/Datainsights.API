using DataInsights.API.DTOs;

namespace DataInsights.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product_DTO>> GetAllProductsAsync();
        Task<Product_DTO?> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product_DTO productDto);
        Task UpdateProductAsync(Product_DTO productDto);
        Task DeleteProductAsync(int productId);
    }
}
