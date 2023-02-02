using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string Username);
    }
}
