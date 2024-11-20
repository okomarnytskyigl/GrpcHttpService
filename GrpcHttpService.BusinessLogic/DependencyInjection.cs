using GrpcHttpService.BusinessLogic.Interfaces.Service;
using GrpcHttpService.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcHttpService.BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
