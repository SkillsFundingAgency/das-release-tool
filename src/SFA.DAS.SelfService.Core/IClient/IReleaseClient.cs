using SFA.DAS.SelfService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.SelfService.Core.IReleases
{
    public interface IReleaseClient
    {
        Task<List<VstsReleaseDefinition>> GetReleases();

        Task<VstsReleaseDefinition> GetRelease(int releaseDefinitionId);

        Task<VstsRelease> CreateRelease(int releaseDefinitionId, Dictionary<string, string> overrideParameters);

        Task<VstsRelease> CreateRelease(int releaseDefinitionId);

        Task<IList<VstsReleaseStatus>> CheckReleaseStatus(int releaseDefinitionId, int releaseId);

        Task StartEnvironmentDeployment(VstsRelease vstsRelease, int releaseEnvironmentId);
    }
}