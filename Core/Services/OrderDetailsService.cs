using Core.Interfaces;
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDetails> GetDetail(Guid id)
        {
            var result = await _unitOfWork.OrderDetailsRepository.GetAsync(p => p.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderDetails>> GetDetails()
        {
            var result = await _unitOfWork.OrderDetailsRepository.ListAllAsync();
            return result;
        }

        public async Task<OrderDetails> InsertDetail(OrderDetails detail)
        {
            await _unitOfWork.OrderDetailsRepository.AddAsync(detail);
            return detail;
        }

        public async Task<OrderDetails> UpdateDetail(OrderDetails detail)
        {
            await _unitOfWork.OrderDetailsRepository.UpdateAsync(detail);
            return detail;
        }

        public async Task DeleteDetail(Guid Id)
        {
            OrderDetails detail = await GetDetail(Id);
            await _unitOfWork.OrderDetailsRepository.DeleteAsync(detail);
        }
    }
}
