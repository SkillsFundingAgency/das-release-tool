using SFA.DAS.ReleaseTool.Core.Entities;

namespace SFA.DAS.ReleaseTool.Web.Models.Whitelist
{
    public class WhitelistReleaseViewModel
    {
        public VstsReleaseStatus deploymentStatus { get; set; }
        public int releaseId { get; set; }
        public int releaseDefinitionId { get; set; }
    }
}
