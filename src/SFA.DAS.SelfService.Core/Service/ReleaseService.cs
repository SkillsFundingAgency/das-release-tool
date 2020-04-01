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

        public async Task<List<VstsReleaseDefinition>> GetReleases()
        {
            return await releaseClient.GetReleases();
        }

        public async Task<VstsReleaseDefinition> GetRelease(string releaseName)
        {
            return await releaseClient.GetRelease(releaseName);
        }

        public async Task<VstsRelease> CreateRelease(int releaseId, Dictionary<string, string> overrideParameters)
        {
            return await releaseClient.CreateRelease(releaseId, overrideParameters);
        }

        public async Task<VstsRelease> CreateRelease(int releaseId)
        {
            return await releaseClient.CreateRelease(releaseId);
        }

        public async Task<VstsReleaseStatus> CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {
            return await releaseClient.CheckReleaseStatus(releaseDefinitionId, releaseId);
        }
    }
}