using Core.Interfaces;
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            var result = await _unitOfWork.CustomerRepository.GetAsync(p => p.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var result = await _unitOfWork.CustomerRepository.ListAllAsync();
            return result;
        }

        public async Task<Customer> InsertCustomer(Customer customers)
        {
            await _unitOfWork.CustomerRepository.AddAsync(customers);
            return customers;
        }

        public async Task<Customer> UpdateCustomer(Customer customers)
        {
            await _unitOfWork.CustomerRepository.UpdateAsync(customers);
            return customers;
        }

        public async Task DeleteCustomer(Guid Id)
        {
            Customer customer = await GetCustomer(Id);
            await _unitOfWork.CustomerRepository.DeleteAsync(customer);
        }
    }
}
