using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoriesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductCategories> GetCategory(Guid id)
        {
            var result = await _unitOfWork.ProductCategoriesRepository.GetAsync(p => p.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductCategories>> GetCategories()
        {
            var result = await _unitOfWork.ProductCategoriesRepository.ListAllAsync();
            return result;
        }

        public async Task<ProductCategories> InsertCategory(ProductCategories category)
        {
            await _unitOfWork.ProductCategoriesRepository.AddAsync(category);
            return category;
        }

        public async Task<ProductCategories> UpdateCategory(ProductCategories category)
        {
            await _unitOfWork.ProductCategoriesRepository.UpdateAsync(category);
            return category;
        }

        public async Task DeleteCategory(Guid Id)
        {
            ProductCategories category = await GetCategory(Id);
            await _unitOfWork.ProductCategoriesRepository.DeleteAsync(category);
        }
    }
}
