using SFA.DAS.SelfService.Core.Entities;
using SFA.DAS.SelfService.Core.IReleases;
using SFA.DAS.SelfService.Core.IServices;
using System.Collections.Generic;

namespace SFA.DAS.SelfService.Core.Services
{
    public class ReleaseService : IReleaseService
    {
        private readonly IReleaseClient releaseClient;

        public ReleaseService(IReleaseClient _releaseClient)
        {
            releaseClient = _releaseClient;
        }

        public List<VstsReleaseDefinition> GetReleases()
        {
            var releases = releaseClient.GetReleases();

            return releases;
        }

        public VstsReleaseDefinition GetRelease(string releaseName)
        {
            var release = releaseClient.GetRelease(releaseName);

            return release;
        }

        public VstsRelease CreateRelease(int releaseId, Dictionary<string, string> overrideParameters)
        {
            var release = releaseClient.CreateRelease(releaseId, overrideParameters);

            return release;
        }

        public VstsRelease CreateRelease(int releaseId)
        {
            var release = releaseClient.CreateRelease(releaseId);

            return release;
        }

        public VstsReleaseStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {
            var releaseStatus = releaseClient.CheckReleaseStatus(releaseDefinitionId, releaseId);

            return releaseStatus;
        }
    }
}