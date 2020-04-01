using SFA.DAS.SelfService.Core.Entities;
using System.Collections.Generic;

namespace SFA.DAS.SelfService.Web.Models.Whitelist
{
    public class WhitelistReleaseViewModel
    {
        public IList<VstsReleaseStatus> deploymentStatus { get; set; }
        public int releaseId { get; set; }
        public int releaseDefinitionId { get; set; }
    }
}