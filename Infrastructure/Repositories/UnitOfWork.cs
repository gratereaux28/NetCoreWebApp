using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NetCoreWebAppContext _context;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IBaseRepository<User> _userRepository;

        public IConfiguration Configuration { get; }

        public UnitOfWork(NetCoreWebAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IBaseRepository<Customer> CustomerRepository => _customerRepository ?? new BaseRepository<Customer>(_context);
        public IBaseRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

    }
}