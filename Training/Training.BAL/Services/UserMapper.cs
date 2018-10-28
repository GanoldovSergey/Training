using Training.BAL.Entities;
using Training.DAL.Entities;

namespace Training.BAL.Services
{
    public static class UserMapper
    {
        public static UserEntity ToBalUser(this UserDto user)
        {
            return new UserEntity
            {
                Id = user?.Id,
                Name = user?.Name,
                Password = user?.Password,
                Role = user == null ? 0 : (Roles)user.Role
            };
        }

        public static UserDto ToDalUser(this UserEntity user)
        {
            return new UserDto
            {
                Id = user?.Id,
                Name = user?.Name,
                Password = user?.Password,
                Role = user == null ? 0 : (int)user.Role
            };
        }
    }
}
