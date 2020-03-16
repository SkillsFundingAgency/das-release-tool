using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ReleaseTool.Core.Configuration;
using SFA.DAS.ReleaseTool.Core.IReleases;
using SFA.DAS.ReleaseTool.Core.IServices;
using SFA.DAS.ReleaseTool.Core.Services;
using SFA.DAS.ReleaseTool.Infrastructure.Releases;

namespace SFA.DAS.ReleaseTool.Web.AppStart
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IReleaseService, ReleaseService>();
            services.AddSingleton<IReleaseClient, ReleaseClient>();
            services.Configure<VstsConfiguration>(configuration.GetSection("VstsConfig"));
        }
    }
}
