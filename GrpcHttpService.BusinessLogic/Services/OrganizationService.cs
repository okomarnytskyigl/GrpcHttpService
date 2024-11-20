using GrpcHttpService.BusinessLogic.Dtos;
using GrpcHttpService.BusinessLogic.Interfaces.Service;
using GrpcHttpService.Domain.Entities;
using GrpcHttpService.Domain.Exceptions;
using GrpcHttpService.Domain.Interfaces;

namespace GrpcHttpService.BusinessLogic.Services
{
    /// <summary>
    /// Organization Service
    /// </summary>
    public class OrganizationService(IOrganizationRepository organizationRepository, IUserRepository userRepository) : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository = organizationRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<OrganizationDto> CreateOrganizationAsync(string name, string address)
        {
            var organization = new Organization(name, address);
            await _organizationRepository.AddAsync(organization);
            return MapToDto(organization);
        }

        public async Task<OrganizationDto?> GetOrganizationByIdAsync(Guid id)
        {
            var organization = await _organizationRepository.GetByIdAsync(id);
            return organization != null ? MapToDto(organization) : null;
        }

        public async Task<IEnumerable<OrganizationDto>> GetOrganizationsAsync(int page, int pageSize, string? query = null)
        {
            var organizations = await _organizationRepository.GetPagedAsync(page, pageSize, query);
            return organizations.Select(MapToDto);
        }

        public async Task AddUserToOrganizationAsync(Guid organizationId, Guid userId)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId)
                                ?? throw new DomainException("Organization not found");

            var user = await _userRepository.GetByIdAsync(userId)
                        ?? throw new DomainException("User not found");

            organization.AddUser(user);
            await _organizationRepository.SaveChangesAsync();
        }

        public async Task RemoveUserFromOrganizationAsync(Guid organizationId, Guid userId)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId)
                                ?? throw new DomainException("Organization not found");
            var user = await _userRepository.GetByIdAsync(userId)
                        ?? throw new DomainException("User not found");

            organization.RemoveUser(user);
            await _organizationRepository.SaveChangesAsync();
        }

        public async Task UpdateOrganizationAsync(Guid id, string name, string address)
        {
            var organization = await _organizationRepository.GetByIdAsync(id)
                                ?? throw new DomainException("Organization not found");

            organization.Update(name, address);
            await _organizationRepository.SaveChangesAsync();
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            var organization = await _organizationRepository.GetByIdAsync(id)
                                ?? throw new DomainException("Organization not found");

            organization.SoftDelete();
            await _organizationRepository.SaveChangesAsync();
        }

        public async Task RestoreOrganizationAsync(Guid id)
        {
            var organization = await _organizationRepository.GetByIdAsync(id, includeDeleted: true)
                                ?? throw new DomainException("Organization not found");

            organization.Restore();
            await _organizationRepository.SaveChangesAsync();
        }

        private static OrganizationDto MapToDto(Organization organization)
        {
            return new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name,
                Address = organization.Address,
                CreatedAt = organization.CreatedAt,
                UpdatedAt = organization.UpdatedAt
            };
        }
    }
}
