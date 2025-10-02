using AutoMapper;
using DataInsights.API.DTOs;
using DataInsights.API.Models;

namespace DataInsights.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product_DTO,Product>().ReverseMap();
            CreateMap<Sale, Sale_DTO>();
            CreateMap<Sale_DTO, Sale>().ReverseMap();
            CreateMap<Customer_DTO, CustomerDTO>().ReverseMap();
        }
    }
}
