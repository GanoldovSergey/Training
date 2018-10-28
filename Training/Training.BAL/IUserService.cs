using System.Collections.Generic;
using System.Threading.Tasks;
using Training.BAL.Entities;

namespace Training.BAL
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(UserEntity user);

        Task DeleteUserAsync(string id);

        Task<UserEntity> GetUserByIdAsync(string id);

        Task<IEnumerable<UserEntity>> GetUsersAsync();

        Task<UserEntity> GetUserAsync(UserEntity user);

        Task<UserResponse> UpdateUserAsync(string id, UserEntity user);

        Task<bool> IsUserExistAsync(UserEntity user);
    }
}
