using GrpcHttpService.BusinessLogic.Dtos;
using GrpcHttpService.BusinessLogic.Interfaces.Service;
using GrpcHttpService.Domain.Entities;
using GrpcHttpService.Domain.Exceptions;
using GrpcHttpService.Domain.Interfaces;

namespace GrpcHttpService.BusinessLogic.Services
{
    /// <summary>
    /// Implementation of business logic for managing users.
    /// </summary>
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto> CreateUserAsync(string name, string username, string email)
        {
            var user = new User(name, username, email);
            await _userRepository.AddAsync(user);
            return MapToDto(user);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync(int page, int pageSize, string? query = null)
        {
            var users = await _userRepository.GetPagedAsync(page, pageSize, query);
            return users.Select(MapToDto);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id)
                        ?? throw new DomainException("User not found");

            user.SoftDelete();
            await _userRepository.SaveChangesAsync();
        }

        public async Task RestoreUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id, includeDeleted: true)
                        ?? throw new DomainException("User not found");

            user.Restore();
            await _userRepository.SaveChangesAsync();
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
