using AutoMapper;
using DataInsights.API.DTOs;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using DataInsights.API.Services.Interfaces;

namespace DataInsights.API.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerServices(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer_DTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return _mapper.Map<IEnumerable<Customer_DTO>>(customers);
        }

        public async Task<Customer_DTO?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return customer == null ? null : _mapper.Map<Customer_DTO>(customer);

            //  customer == null ? false : true;
        }

        public async Task<Customer_DTO> AddCustomerAsync(Customer_DTO customerDto)
        {
            var customer = _mapper.Map/* Change this to Customer_DTO if experiencing mismatch when reaching this method*/<CustomerDTO>(customerDto);
            var addedCustomer = await _customerRepository.AddCustomerAsync(customer);
            return _mapper.Map<Customer_DTO>(addedCustomer);
        }

        public async Task<Customer_DTO?> UpdateCustomerAsync(int id, Customer_DTO customerDto)
        {
            var customer = _mapper.Map<CustomerDTO>(customerDto);
            var updatedCustomer = await _customerRepository.UpdateCustomerAsync(id, customer);
            return updatedCustomer == null ? null : _mapper.Map<Customer_DTO>(updatedCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var deleted = _customerRepository.DeleteCustomerAsync(id);
            if (deleted == null)
            {
                return false;
            }

           return await deleted;
        }
    }
}
