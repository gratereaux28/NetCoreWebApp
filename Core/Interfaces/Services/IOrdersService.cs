using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrdersService
    {
        Task<Orders> GetOrder(Guid id);
        Task<IEnumerable<Orders>> GetOrders();
        Task<Orders> InsertOrders(Orders order);
        Task<Orders> UpdateOrders(Orders order);
        Task DeleteOrders(Guid Id);
    }
}
