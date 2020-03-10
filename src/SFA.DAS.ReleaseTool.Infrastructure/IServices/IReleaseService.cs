using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using System.Collections.Generic;

namespace SFA.DAS.ReleaseTool.Core.IServices
{
    public interface IReleaseService
    {
        List<ReleaseDefinition> GetReleases();

        ReleaseDefinition GetRelease(string releaseName);

        Release CreateRelease(int releaseDefinitionId, string ipAddress);

        DeploymentStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId);
    }
}
