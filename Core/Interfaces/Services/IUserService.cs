using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<Users> GetUser(string Username);
    }
}
