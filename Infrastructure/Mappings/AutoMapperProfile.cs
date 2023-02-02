using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}