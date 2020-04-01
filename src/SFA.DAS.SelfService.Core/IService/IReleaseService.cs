using SFA.DAS.SelfService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.SelfService.Core.IServices
{
    public interface IReleaseService
    {
        Task<List<VstsReleaseDefinition>> GetReleases();

        Task<VstsReleaseDefinition> GetRelease(string releaseName);

        Task<VstsRelease> CreateRelease(int releaseDefinitionId, Dictionary<string, string> overrideParameters);

        Task<VstsRelease> CreateRelease(int releaseDefinitionId);

        Task<VstsReleaseStatus> CheckReleaseStatus(int releaseDefinitionId, int releaseId);
    }
}