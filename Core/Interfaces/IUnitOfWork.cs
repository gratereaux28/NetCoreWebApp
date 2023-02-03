using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Customers> CustomerRepository { get; }

        public IBaseRepository<Orders> OrdersRepository { get; }
        public IBaseRepository<OrderDetails> OrderDetailsRepository { get; }
        public IBaseRepository<Products> ProductsRepository { get; }
        public IBaseRepository<ProductCategories> ProductCategoriesRepository { get; }
        public IBaseRepository<Users> UserRepository { get; }
    }
}
