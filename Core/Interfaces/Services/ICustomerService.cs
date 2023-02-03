using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Customers> GetCustomer(Guid id);
        Task<IEnumerable<Customers>> GetCustomers();
        Task<Customers> InsertCustomer(Customers customers);
        Task<Customers> UpdateCustomer(Customers customers);
        Task DeleteCustomer(Guid Id);
    }
}
