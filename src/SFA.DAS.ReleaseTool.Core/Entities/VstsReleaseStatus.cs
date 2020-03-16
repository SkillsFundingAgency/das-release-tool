using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ReleaseTool.Core.Entities
{
    public enum VstsReleaseStatus
    {
        Undefined = 0,
        NotDeployed = 1,
        InProgress = 2,
        Succeeded = 4,
        PartiallySucceeded = 8,
        Failed = 16,
        All = 31
    }
}
