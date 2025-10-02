using DataInsights.API.DTOs;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using DataInsights.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataInsights.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("All-sales")]

        public async Task<ActionResult<IEnumerable<Sale_DTO>>> GetAllSales()
        {
            var sales = await _salesService.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpGet("GetBy{id}")]
        public async Task<IActionResult> GetSalesById(int id)
        {
            var sale = await _salesService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        [HttpPost("Add-sale")]
        public async Task<IActionResult> AddSale([FromBody] Sale_DTO saleDTO)
        {
            await _salesService.AddSaleAsync(saleDTO);
            return CreatedAtAction(nameof(GetSalesById), new { id = saleDTO.Sales_ID }, saleDTO);
        }

        [HttpPut("Update-sale")]
        public async Task<IActionResult> UpdateSale([FromBody] Sale_DTO saleDTO)
        {
            var existingSale = await _salesService.GetSaleByIdAsync(saleDTO.Sales_ID);
            if (existingSale == null)
            {
                return NotFound();
            }
            await _salesService.UpdateSaleAsync(saleDTO.Sales_ID, saleDTO);
            return NoContent();
        }


        [HttpDelete("Delete-sale{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var existingSale = await _salesService.GetSaleByIdAsync(id);
            if (existingSale == null)
            {
                return NotFound();
            }
            await _salesService.DeleteSaleAsync(id);
            return NoContent();
        }
    }
}
