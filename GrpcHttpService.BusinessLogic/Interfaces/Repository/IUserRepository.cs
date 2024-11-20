using GrpcHttpService.Domain.Entities;

namespace GrpcHttpService.Domain.Interfaces
{
    /// <summary>
    /// User Repository
    /// </summary>
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<IEnumerable<User>> GetPagedAsync(int page, int pageSize, string? query = null);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
