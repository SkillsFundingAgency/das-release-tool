using SFA.DAS.SelfService.Core.Entities;
using SFA.DAS.SelfService.Core.IReleases;
using SFA.DAS.SelfService.Core.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.SelfService.Core.Services
{
    public class ReleaseService : IReleaseService
    {
        private readonly IReleaseClient releaseClient;

        public ReleaseService(IReleaseClient _releaseClient)
        {
            releaseClient = _releaseClient;
        }

        public Task<List<VstsReleaseDefinition>> GetReleasesAsync()
        {
            var releases = releaseClient.GetReleasesAsync();

            return releases;
        }

        public Task<VstsReleaseDefinition> GetReleaseAsync(int releaseDefinitionId)
        {
            var release = releaseClient.GetReleaseAsync(releaseDefinitionId);

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

        public IList<VstsReleaseStatus> CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {
            var releaseStatus = releaseClient.CheckReleaseStatus(releaseDefinitionId, releaseId);

            return releaseStatus;
        }

        public void StartEnvironmentDeployment(VstsRelease vstsRelease, int releaseEnvironmentId)
        {
            releaseClient.StartEnvironmentDeployment(vstsRelease, releaseEnvironmentId);
        }
    }
}