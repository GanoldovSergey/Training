using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BAL.Entities;
using Training.DAL.Exceptions;
using Training.DAL.Services;
using Training.DAL.Services.Interfaces;

namespace Training.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogService _logService;
        
        public UserService(IUserRepository userRepository, ILogService logService)
        {
            _userRepository = userRepository;
            _logService = logService;
            _logService.InfoWriteToLog(DateTime.Now, Resource.LogUserServiceCreating, Resource.LogUserServiceCreated);
        }

        public async Task<UserResponse> CreateUserAsync(UserEntity user)
        {
            try
            {
                user.Role = Roles.Student;
                await _userRepository.CreateUserAsync(user.ToDalUser());
                _logService.InfoWriteToLog(DateTime.Now, String.Format(Resource.LogUserCreating, user.Name), String.Format(Resource.LogUserCreated, user.Name));
                return new UserResponse { Success = true };
            }
            catch (UserExistException exception)
            {
                _logService.ErrorWriteToLog(DateTime.Now,
                    String.Format(Resource.LogErrorExeptionMessage, GetType().Name, exception.GetType().Name, exception.Message),
                    String.Format(Resource.LogErrorExeptionCatched, exception.GetType().Name));
                return new UserResponse { ErrorMessage = String.Format(Resource.UserResponseErrorMessage, user.Name), Success = false };
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            _logService.InfoWriteToLog(DateTime.Now, String.Format(Resource.LogUserDeleting, id), String.Format(Resource.LogUserDeleted, id));
        }

        public async Task<UserResponse> UpdateUserAsync(string id, UserEntity user)
        {
            try
            {
                await _userRepository.UpdateUserAsync(id, user.ToDalUser());
                _logService.InfoWriteToLog(DateTime.Now, String.Format(Resource.LogUserUpdating, id), String.Format(Resource.LogUserUpdated, id));
                return new UserResponse { Success = true };
            }
            catch (UserExistException exception)
            {
                _logService.ErrorWriteToLog(DateTime.Now,
                    String.Format(Resource.LogErrorExeptionMessage, GetType().Name, exception.GetType().Name, exception.Message),
                    String.Format(Resource.LogErrorExeptionCatched, exception.GetType().Name));
                return new UserResponse { ErrorMessage = String.Format(Resource.UserResponseErrorMessage, user.Name), Success = false };
            }
        }

        public async Task<UserEntity> GetUserByIdAsync(string id)
        {
            var result = await _userRepository.GetUserByIdAsync(id);
            _logService.InfoWriteToLog(DateTime.Now, String.Format(Resource.LogUserGetting, id), String.Format(Resource.LogUserFound, id));
            return result.ToBalUser();
        }

        public async Task<UserEntity> GetUserAsync(UserEntity user)
        {
            var result = await _userRepository.GetUserAsync(user.ToDalUser());
            return result.ToBalUser();
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            _logService.InfoWriteToLog(DateTime.Now, Resource.LogUsersGetting, Resource.LogUsersFound);
            return users?.Where(user => user != null)
                .Select(user => user.ToBalUser());
        }

        public async Task<bool> IsUserExistAsync(UserEntity user)
        {
            return await _userRepository.IsUserExistAsync(user.ToDalUser());
        }
    }
}
