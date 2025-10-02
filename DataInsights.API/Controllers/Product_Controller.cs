using DataInsights.API.Data;
using DataInsights.API.DTOs;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using DataInsights.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DataInsights.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Product_Controller : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<Product_Controller> _logger;
       
        public Product_Controller(IProductService productService, ILogger<Product_Controller> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet("All-sales")]

        public async Task<ActionResult<IEnumerable<Product_DTO>>> GetAllProducts()
        {
            _logger.LogInformation("Retrieving all products....");
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("/sales/add")]

        public async Task<IActionResult> AddProduct([FromBody] Product_DTO productDTO)
        {
            await _productService.AddProductAsync(productDTO);
            return CreatedAtAction(nameof(GetProductById), new { id = productDTO.Product_Id }, productDTO);
        }

        [HttpPut("/sales/update")]

        public async Task<IActionResult> UpdateProduct([FromBody] Product_DTO productDTO)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productDTO.Product_Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productService.UpdateProductAsync(productDTO);
            return NoContent();
        }

        [HttpDelete("/sales/delete/{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
