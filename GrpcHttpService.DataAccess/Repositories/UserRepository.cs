using GrpcHttpService.Domain.Entities;
using GrpcHttpService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcHttpService.DataAccess.Repositories
{
    /// <summary>
    /// Repository for users
    /// </summary>
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var users = _context.Users.AsQueryable();
            if (!includeDeleted)
            {
                users = users.Where(u => u.DeletedAt == null);
            }
            return await users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetPagedAsync(int page, int pageSize, string? query = null)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                users = users.Where(u => u.Name.Contains(query) || u.Username.Contains(query) || u.Email.Contains(query));
            }

            return await users
                .Where(u => u.DeletedAt == null)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
