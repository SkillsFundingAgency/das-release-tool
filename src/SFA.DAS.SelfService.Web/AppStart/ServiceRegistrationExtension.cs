using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using SFA.DAS.SelfService.Core.Configuration;
using SFA.DAS.SelfService.Core.IReleases;
using SFA.DAS.SelfService.Core.IServices;
using SFA.DAS.SelfService.Core.Services;
using SFA.DAS.SelfService.Infrastructure.Releases;
using SFA.DAS.SelfService.Web.Infrastructure;

namespace SFA.DAS.SelfService.Web.AppStart
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IReleaseService, ReleaseService>();
            services.AddSingleton<IReleaseClient, ReleaseClient>();
            services.Configure<VstsConfiguration>(configuration.GetSection("VstsConfig"));
            services.AddSingleton<ITelemetryInitializer, ToolsTelemetryInitializer>();
        }
    }
}
