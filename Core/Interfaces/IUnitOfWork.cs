using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Customer> CustomerRepository { get; }

        public IBaseRepository<User> UserRepository { get; }
    }
}
