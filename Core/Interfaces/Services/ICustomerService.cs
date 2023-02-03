using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(Guid id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> InsertCustomer(Customer customers);
        Task<Customer> UpdateCustomer(Customer customers);
        Task DeleteCustomer(Guid Id);
    }
}
