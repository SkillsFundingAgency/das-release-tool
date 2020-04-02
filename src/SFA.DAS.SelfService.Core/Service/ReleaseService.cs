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

        public async Task<List<VstsReleaseDefinition>> GetReleasesAsync()
        {
            return await releaseClient.GetReleases(); ;
        }

        public async Task<VstsReleaseDefinition> GetReleaseAsync(int releaseDefinitionId)
        {
            return await releaseClient.GetRelease(releaseDefinitionId);
        }

        public async Task<VstsRelease> CreateRelease(int releaseId, Dictionary<string, string> overrideParameters)
        {
            return await releaseClient.CreateRelease(releaseId, overrideParameters);
        }

        public async Task<VstsRelease> CreateRelease(int releaseId)
        {
            return await releaseClient.CreateRelease(releaseId);
        }

        public async Task<IList<VstsReleaseStatus>> CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {

            return await releaseClient.CheckReleaseStatus(releaseDefinitionId, releaseId);
        }

        public async Task StartEnvironmentDeployment(VstsRelease vstsRelease, int releaseEnvironmentId)
        {
             await releaseClient.StartEnvironmentDeployment(vstsRelease, releaseEnvironmentId);
        }
    }
}