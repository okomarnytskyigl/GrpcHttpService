using GrpcHttpService.BusinessLogic.Dtos;

namespace GrpcHttpService.BusinessLogic.Interfaces.Service
{
    /// <summary>
    /// Interface for organization service
    /// </summary>
    public interface IOrganizationService
    {
        Task<OrganizationDto> CreateOrganizationAsync(string name, string address);
        Task<OrganizationDto?> GetOrganizationByIdAsync(Guid id);
        Task<IEnumerable<OrganizationDto>> GetOrganizationsAsync(int page, int pageSize, string? query = null);
        Task AddUserToOrganizationAsync(Guid organizationId, Guid userId);
        Task RemoveUserFromOrganizationAsync(Guid organizationId, Guid userId);
        Task UpdateOrganizationAsync(Guid id, string name, string address);
        Task DeleteOrganizationAsync(Guid id);
        Task RestoreOrganizationAsync(Guid id);
    }
}
