using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Orders> GetOrder(Guid id)
        {
            var result = await _unitOfWork.OrdersRepository.AddInclude("Customer").AddInclude("OrderDetails").GetAsync(p => p.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Orders>> GetOrders()
        {
            var result = await _unitOfWork.OrdersRepository.AddInclude("OrderDetails").ListAllAsync();
            return result;
        }

        public async Task<Orders> InsertOrders(Orders order)
        {
            await _unitOfWork.OrdersRepository.AddAsync(order);
            return order;
        }

        public async Task<Orders> UpdateOrders(Orders order)
        {
            await _unitOfWork.OrdersRepository.UpdateAsync(order);
            return order;
        }

        public async Task DeleteOrders(Guid Id)
        {
            Orders order = await GetOrder(Id);
            await _unitOfWork.OrdersRepository.DeleteAsync(order);
        }
    }
}
