using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using System.Collections.Generic;

namespace SFA.DAS.ReleaseTool.Infrastructure.IReleases
{
    public interface IReleaseClient
    {
        List<ReleaseDefinition> GetReleases();

        ReleaseDefinition GetRelease(string releaseName);

        Release CreateRelease(int releaseDefinitionId, string ipAddress);

        DeploymentStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId);
    }
}
