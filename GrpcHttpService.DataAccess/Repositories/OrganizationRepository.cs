using GrpcHttpService.Domain.Entities;
using GrpcHttpService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcHttpService.DataAccess.Repositories
{
    /// <summary>
    /// Repository for organizations
    /// </summary>
    public class OrganizationRepository(AppDbContext context) : IOrganizationRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Organization?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var query = _context.Organizations.AsQueryable();
            if (!includeDeleted)
            {
                query = query.Where(o => o.DeletedAt == null);
            }
            return await query.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Organization>> GetPagedAsync(int page, int pageSize, string? query = null)
        {
            var organizations = _context.Organizations.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                organizations = organizations.Where(o => o.Name.Contains(query));
            }

            return await organizations
                .Where(o => o.DeletedAt == null)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
