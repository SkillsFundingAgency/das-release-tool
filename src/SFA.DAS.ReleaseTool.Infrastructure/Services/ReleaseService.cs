using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using SFA.DAS.ReleaseTool.Core.IServices;
using SFA.DAS.ReleaseTool.Infrastructure.IReleases;
using System.Collections.Generic;

namespace SFA.DAS.ReleaseTool.Core.Services
{
    public class ReleaseService : IReleaseService
    {
        private readonly IReleaseClient releaseClient;

        public ReleaseService(IReleaseClient _releaseClient)
        {
            releaseClient = _releaseClient;
        }

        public List<ReleaseDefinition> GetReleases()
        {
            var releases = releaseClient.GetReleases();

            return releases;
        }

        public ReleaseDefinition GetRelease(string releaseName)
        {
            var release = releaseClient.GetRelease(releaseName);

            return release;
        }

        public Release CreateRelease(int releaseId, string ipAddress)
        {
            var release = releaseClient.CreateRelease(releaseId, ipAddress);

            return release;
        }

        public DeploymentStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {
            var releaseStatus = releaseClient.CheckReleaseStatus(releaseDefinitionId, releaseId);

            return releaseStatus;
        }
    }
}
