using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductsService
    {
        Task<Products> GetProduct(Guid id);
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> InsertProduct(Products product);
        Task<Products> UpdateProduct(Products product);
        Task DeleteProduct(Guid Id);
    }
}
