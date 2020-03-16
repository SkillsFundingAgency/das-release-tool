using SFA.DAS.ReleaseTool.Core.Entities;
using System.Collections.Generic;

namespace SFA.DAS.ReleaseTool.Core.IServices
{
    public interface IReleaseService
    {
        List<VstsReleaseDefinition> GetReleases();

        VstsReleaseDefinition GetRelease(string releaseName);

        VstsRelease CreateRelease(int releaseDefinitionId, Dictionary<string, string> overrideParameters);

        VstsRelease CreateRelease(int releaseDefinitionId);

        VstsReleaseStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId);
    }
}