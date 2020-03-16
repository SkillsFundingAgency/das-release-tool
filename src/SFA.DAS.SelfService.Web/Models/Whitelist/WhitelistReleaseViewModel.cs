using SFA.DAS.SelfService.Core.Entities;

namespace SFA.DAS.SelfService.Web.Models.Whitelist
{
    public class WhitelistReleaseViewModel
    {
        public VstsReleaseStatus deploymentStatus { get; set; }
        public int releaseId { get; set; }
        public int releaseDefinitionId { get; set; }
    }
}