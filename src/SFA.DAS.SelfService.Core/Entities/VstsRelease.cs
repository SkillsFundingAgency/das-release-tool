using System.Collections.Generic;

namespace SFA.DAS.SelfService.Core.Entities
{
    public class VstsRelease
    {
        public int Id { get; set; }
        public int ReleaseDefininitionId { get; set; }
        public string ReleaseName { get; set; }
        public IList<VstsEnvironment> ReleaseEnvironments { get; set; }
    }
}