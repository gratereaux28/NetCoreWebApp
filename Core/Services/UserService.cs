using Core.Interfaces;
using Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Users> GetUser(string Username)
        {
            var result = await _unitOfWork.UserRepository.GetAsync(p => p.Username == Username);
            return result.FirstOrDefault();
        }
    }
}
