using System.ComponentModel;

namespace SFA.DAS.SelfService.Core.Entities
{
    public class VstsReleaseStatus
    {
        public enum Status
        {
            [Description("Fetching Status")]
            Undefined = 0,

            [Description("Queued")]
            NotDeployed = 1,

            [Description("In Progress")]
            InProgress = 2,

            [Description("Complete")]
            Succeeded = 4,

            [Description("Error")]
            PartiallySucceeded = 8,

            Failed = 16,
            All = 31
        }

        public Status ReleaseStatus { get; set; }

        public string Name { get; set; }
    }
}