using System.Collections.Generic;
using System.Threading.Tasks;
using Training.DAL.Entities;

namespace Training.DAL.Services.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(UserDto user);

        Task DeleteUserAsync(string id);

        Task<UserDto> GetUserByIdAsync(string id);

        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task<UserDto> GetUserAsync(UserDto user);

        Task UpdateUserAsync(string id, UserDto user);
    
        Task<bool> IsUserExistAsync(UserDto user);
    }
}
