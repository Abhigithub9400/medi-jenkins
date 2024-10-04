using MediAssist.Infrastructure.Abstract.Configurations;
using Microsoft.Extensions.Configuration;

namespace MediAssist.Configurations
{
    public class Appsettings(IConfiguration configuration) : IAppSettings
    {
        public string ApiKey => configuration["FireBase:ApiKey"] ?? string.Empty;
    }
}
