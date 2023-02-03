using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Customers> CustomerRepository { get; }

        public IBaseRepository<Users> UserRepository { get; }
    }
}
