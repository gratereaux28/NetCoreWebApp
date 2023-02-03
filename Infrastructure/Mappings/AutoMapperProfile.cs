using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace Infrastructure.Mappings
{
    // In this class wee use AutoMapper to create a map between some models
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customers, CustomersDTO>().ReverseMap();
            CreateMap<Orders, OrdersDTO>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
            CreateMap<Products, ProductsDTO>().ReverseMap();
            CreateMap<ProductCategories, ProductCategoriesDTO>().ReverseMap();
            CreateMap<Users, UsersDTO>().ReverseMap();
        }
    }
}