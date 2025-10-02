using DataInsights.API.Data;
using DataInsights.API.DTOs;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using DataInsights.API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DataInsights.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        
        }
        [HttpGet("All-Customers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customer = await _customerService.GetAllCustomersAsync();
            return Ok(customer);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customer);
        }
        [HttpPost("/customers/add")]
        public async Task<IActionResult> AddCustomer( Customer_DTO customer, int id)
        {
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new {  id = customer._customerName }, customer);
        }
        [HttpPut("/customers/update")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer_DTO customer)
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            await _customerService.UpdateCustomerAsync(id, customer);
            return NoContent();
        }
        [HttpDelete("/customers/delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
