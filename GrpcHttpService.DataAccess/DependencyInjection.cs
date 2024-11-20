using GrpcHttpService.DataAccess.Repositories;
using GrpcHttpService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GrpcHttpService.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection services,
                                                                        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GrpcHttpServiceDB") ??
                throw new InvalidOperationException("Connection string 'GrpcHttpServiceDB' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            using (var scope = services.BuildServiceProvider().CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                context.Database.EnsureCreated();

            return services;
        }
    }
}
