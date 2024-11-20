using GrpcHttpService.BusinessLogic.Dtos;

namespace GrpcHttpService.BusinessLogic.Interfaces.Service
{
    /// <summary>
    /// Interface for user service
    /// </summary>
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(string name, string username, string email);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetUsersAsync(int page, int pageSize, string? query = null);
        Task DeleteUserAsync(Guid id);
        Task RestoreUserAsync(Guid id);
    }
}
