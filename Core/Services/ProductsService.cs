using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Products> GetProduct(Guid id)
        {
            var result = await _unitOfWork.ProductsRepository.AddInclude("Category").GetAsync(p => p.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            var result = await _unitOfWork.ProductsRepository.ListAllAsync();
            return result;
        }

        public async Task<Products> InsertProduct(Products product)
        {
            await _unitOfWork.ProductsRepository.AddAsync(product);
            return product;
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            await _unitOfWork.ProductsRepository.UpdateAsync(product);
            return product;
        }

        public async Task DeleteProduct(Guid Id)
        {
            Products product = await GetProduct(Id);
            await _unitOfWork.ProductsRepository.DeleteAsync(product);
        }
    }
}
