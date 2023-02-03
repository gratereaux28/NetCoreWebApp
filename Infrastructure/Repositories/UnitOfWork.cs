﻿using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NetCoreWebAppContext _context;
        private readonly IBaseRepository<Customers> _customerRepository;
        private readonly IBaseRepository<Products> _productsRepository;
        private readonly IBaseRepository<ProductCategories> _productCategoriesRepository;
        private readonly IBaseRepository<Users> _userRepository;

        public IConfiguration Configuration { get; }

        public UnitOfWork(NetCoreWebAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IBaseRepository<Customers> CustomerRepository => _customerRepository ?? new BaseRepository<Customers>(_context);
        public IBaseRepository<Products> ProductsRepository => _productsRepository ?? new BaseRepository<Products>(_context);
        public IBaseRepository<ProductCategories> ProductCategoriesRepository => _productCategoriesRepository ?? new BaseRepository<ProductCategories>(_context);
        public IBaseRepository<Users> UserRepository => _userRepository ?? new BaseRepository<Users>(_context);

    }
}