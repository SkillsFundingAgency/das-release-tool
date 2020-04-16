using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace SFA.DAS.SelfService.Web.Infrastructure
{
    public class ToolsTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                telemetry.Context.Cloud.RoleName = "das-self-service-web";
                //telemetry.Context.Cloud.RoleInstance = "Custom RoleInstance";
            }
        }
    }
}
