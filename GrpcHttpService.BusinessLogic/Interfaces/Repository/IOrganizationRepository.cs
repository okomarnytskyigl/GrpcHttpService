using GrpcHttpService.Domain.Entities;

namespace GrpcHttpService.Domain.Interfaces
{
    /// <summary>
    /// Organization Repository
    /// </summary>
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<IEnumerable<Organization>> GetPagedAsync(int page, int pageSize, string? query = null);
        Task AddAsync(Organization organization);
        Task SaveChangesAsync();
    }
}
