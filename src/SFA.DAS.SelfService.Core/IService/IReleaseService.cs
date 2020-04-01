using SFA.DAS.SelfService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.SelfService.Core.IServices
{
    public interface IReleaseService
    {
        Task<List<VstsReleaseDefinition>> GetReleasesAsync();

        Task<VstsReleaseDefinition> GetReleaseAsync(int releaseDefinitionId);

        Task<VstsRelease> CreateRelease(int releaseDefinitionId, Dictionary<string, string> overrideParameters);

        Task<VstsRelease> CreateRelease(int releaseDefinitionId);

        Task<IList<VstsReleaseStatus>> CheckReleaseStatus(int releaseDefinitionId, int releaseId);

        Task StartEnvironmentDeployment(VstsRelease vstsRelease, int releaseEnvironmentId);
    }
}