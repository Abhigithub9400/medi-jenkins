using MediAssist.Application.Abstract.Services;
using MediAssist.Application.Services;
using MediAssist.Configurations;
using MediAssist.Infrastructure.Abstract.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace MediAssist.Dependency
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppSettings, Appsettings>();
            services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();
            return services;
        }
    }
}
