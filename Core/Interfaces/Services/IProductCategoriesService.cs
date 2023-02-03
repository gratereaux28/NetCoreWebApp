using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductCategoriesService
    {
        Task<ProductCategories> GetCategory(Guid id);
        Task<IEnumerable<ProductCategories>> GetCategories();
        Task<ProductCategories> InsertCategory(ProductCategories category);
        Task<ProductCategories> UpdateCategory(ProductCategories category);
        Task DeleteCategory(Guid Id);
    }
}
