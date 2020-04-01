using SFA.DAS.SelfService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.SelfService.Core.IReleases
{
    public interface IReleaseClient
    {
        Task<List<VstsReleaseDefinition>> GetReleasesAsync();

        Task<VstsReleaseDefinition> GetReleaseAsync(int releaseDefinitionId);

        VstsRelease CreateRelease(int releaseDefinitionId, Dictionary<string, string> overrideParameters);

        VstsRelease CreateRelease(int releaseDefinitionId);

        IList<VstsReleaseStatus> CheckReleaseStatus(int releaseDefinitionId, int releaseId);

        void StartEnvironmentDeployment(VstsRelease vstsRelease, int releaseEnvironmentId);
    }
}