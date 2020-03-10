using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace SFA.DAS.ReleaseTool.Web.Models.Whitelist
{
    public class WhitelistViewModel
    {
        public DeploymentStatus deploymentStatus { get; set; }
        public int releaseId { get; set; }
        public int releaseDefinitionId { get; set; }
    }
}
