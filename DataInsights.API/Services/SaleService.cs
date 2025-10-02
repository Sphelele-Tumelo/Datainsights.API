using AutoMapper;
using DataInsights.API.DTOs;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using DataInsights.API.Services.Interfaces;

namespace DataInsights.API.Services
{
    public class SaleService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        public SaleService(ISalesRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sale_DTO>> GetAllSalesAsync()
        {
            var sales = await _salesRepository.GetAllSalesAsync();
            return _mapper.Map<IEnumerable<Sale_DTO>>(sales);
        }

        public async Task<Sale_DTO?> GetSaleByIdAsync(int id)
        {
            var sale = await _salesRepository.GetSaleByIdAsync(id);
            return sale == null ? null : _mapper.Map<Sale_DTO>(sale);
        }

        public async Task<Sale_DTO> AddSaleAsync(Sale_DTO saleDTO)
        {
            var sale = _mapper.Map<Sale>(saleDTO);
            var addedSale = await _salesRepository.AddSaleAsync(sale);
            return _mapper.Map<Sale_DTO>(addedSale);
        }

        public async Task<Sale_DTO?> UpdateSaleAsync(int id, Sale_DTO saleDto)
        {
            var sale = _mapper.Map<Models.Sale>(saleDto);
            var updatedSale = await _salesRepository.UpdateSaleAsync(id, sale);
            return updatedSale == null ? null : _mapper.Map<Sale_DTO>(updatedSale);
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var deleted = _salesRepository.DeleteSaleAsync(id);
            if (deleted == null)
            {
                return false;
            }
            return await deleted;
        }
    }
}
