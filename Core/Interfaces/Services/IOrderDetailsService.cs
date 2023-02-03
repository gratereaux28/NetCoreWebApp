using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderDetailsService
    {
        Task<OrderDetails> GetDetail(Guid id);
        Task<IEnumerable<OrderDetails>> GetDetails();
        Task<IEnumerable<OrderDetails>> GetDetailsByOrderId(Guid orderId);
        Task<OrderDetails> InsertDetail(OrderDetails detail);
        Task<OrderDetails> UpdateDetail(OrderDetails detail);
        Task DeleteDetail(Guid Id);
    }
}
