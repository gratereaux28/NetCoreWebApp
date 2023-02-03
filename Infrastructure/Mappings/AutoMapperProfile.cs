﻿using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace Infrastructure.Mappings
{
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