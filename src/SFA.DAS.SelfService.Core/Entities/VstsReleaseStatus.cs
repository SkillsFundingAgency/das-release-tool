using System.ComponentModel;

namespace SFA.DAS.SelfService.Core.Entities
{
    public enum VstsReleaseStatus
    {
        [Description("Fetching Status")]
        Undefined = 0,
        [Description("Queued")]
        NotDeployed = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Whitelisted")]
        Succeeded = 4,
        [Description("Whitelisted")]
        PartiallySucceeded = 8,
        Failed = 16,
        All = 31
    }
}