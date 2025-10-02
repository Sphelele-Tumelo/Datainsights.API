using AutoMapper;
using DataInsights.API.DTOs;
using DataInsights.API.Models;
using DataInsights.API.Repository;
using DataInsights.API.Repository.Interfaces;
using DataInsights.API.Services.Interfaces;
using Serilog;
using System.Diagnostics.Eventing.Reader;

namespace DataInsights.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Product_DTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            _logger.LogInformation("Retrieved {Count} products from the repository.", products.Count());
            return _mapper.Map<IEnumerable<Product_DTO>>(products);
        }

        public async Task<Product_DTO?> GetProductByIdAsync(int productId)
        {
            var _product = await _productRepository.GetProductByIdAsync(productId);
            if (_product == null)
            {
                return null;
            }
            ;
            _logger.LogInformation("Product with ID {ProductId} retrieved from the repository.", productId);
            return _mapper.Map<Product_DTO>(_product);
        }

        public async Task AddProductAsync(Product_DTO product_DTO)
        {
            var newProduct = _mapper.Map<Product>(product_DTO);
            Log.Information("New product added with ID {@ProductId}.", newProduct);
            await _productRepository.AddProductAsync(newProduct);
        }

        public async Task UpdateProductAsync(Product_DTO product_DTO)
        {
          var updated = await _productRepository.GetProductByIdAsync(product_DTO.Product_Id);
            if (updated == null)
            {
                throw new Exception("Product Not found!");
            }
            var _product = _mapper.Map<Product>(product_DTO);
            await _productRepository.UpdateProductAsync(_product);

        }

        public async Task DeleteProductAsync(int productId)
        {
            var _existingProduct = await _productRepository.GetProductByIdAsync(productId);

            if (_existingProduct == null)
            {
                throw new Exception("Product Not found!");
            }

            await _productRepository.DeleteProductAsync(productId);
        }
    }
}

/// Summary : This code defines a ProductService class that implements the IProductService interface. It uses an IProductRepository to perform CRUD operations on Product entities and AutoMapper to map between Product and Product_DTO objects. The service methods include getting all products, getting a product by ID, adding a new product, updating an existing product, and deleting a product.
///  It handles cases where a product is not found by returning null or throwing exceptions as appropriate.
///  if DeleteProductAsync is called and doesn't work as it should, it'll likely be the AUTOMAPPER mapping or the repository method itself.