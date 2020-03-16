using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;
using Microsoft.VisualStudio.Services.WebApi;
using SFA.DAS.ReleaseTool.Core.Configuration;
using SFA.DAS.ReleaseTool.Core.Entities;
using SFA.DAS.ReleaseTool.Core.IReleases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ReleaseTool.Infrastructure.Releases
{
    public class ReleaseClient : IReleaseClient
    {
        private readonly VssConnection _vssConnection;
        private readonly IOptions<VstsConfiguration> _configuration;

        public ReleaseClient(IOptions<VstsConfiguration> configuration)
        {
            _configuration = configuration;

            _vssConnection = SetConnection();
        }

        private VssConnection SetConnection()
        {
            VssCredentials creds = new VssBasicCredential(string.Empty, _configuration.Value.PatToken);

            VssConnection connection = new VssConnection(new Uri(_configuration.Value.CollectionUri), creds);

            return connection;
        }

        public List<VstsReleaseDefinition> GetReleases()
        {
            var client = _vssConnection.GetClient<ReleaseHttpClient>();

            var releases = client.GetReleaseDefinitionsAsync(_configuration.Value.ProjectName, expand: ReleaseDefinitionExpands.Variables).Result;

            var vstsReleases = releases
                .Select(r => new VstsReleaseDefinition
                {
                    Id = r.Id,
                    ReleaseName = r.Name
                }).ToList();

            return vstsReleases;
        }

        public VstsReleaseDefinition GetRelease(string releaseName)
        {
            var client = _vssConnection.GetClient<ReleaseHttpClient>();

            var release = client.GetReleaseDefinitionsAsync(_configuration.Value.ProjectName, releaseName, expand: ReleaseDefinitionExpands.Variables).Result.FirstOrDefault();

            var vstsRelease = new VstsReleaseDefinition
            {
                Id = release.Id,
                ReleaseName = release.Name
            };

            return vstsRelease;
        }

        public VstsReleaseStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {
            ReleaseHttpClient releaseClient = _vssConnection.GetClient<ReleaseHttpClient>();

            List<Deployment> deployments = releaseClient.GetDeploymentsAsync(project: _configuration.Value.ProjectName, definitionId: releaseDefinitionId, top: 5).Result;

            var deployment = deployments.Single(r => r.Id == releaseId);

            VstsReleaseStatus vstsReleaseStatus = (VstsReleaseStatus)deployment.DeploymentStatus;

            return vstsReleaseStatus;
        }

        public VstsRelease CreateRelease(int releaseDefinitionId, Dictionary<string, string> overrideParameters)
        {
            ReleaseHttpClient releaseClient = _vssConnection.GetClient<ReleaseHttpClient>();

            Dictionary<string, ConfigurationVariableValue> overRideReleaseLevelVariables = new Dictionary<string, ConfigurationVariableValue>();

            foreach (var overrideParameter in overrideParameters)
            {
                ConfigurationVariableValue overrideVaraible = new ConfigurationVariableValue();
                overrideVaraible.Value = overrideParameter.Value;
                overrideVaraible.IsSecret = false;
                overRideReleaseLevelVariables.Add(overrideParameter.Key, overrideVaraible);
            }

            var release = CreateRelease(releaseClient, releaseDefinitionId, _configuration.Value.ProjectName, overRideReleaseLevelVariables);

            return release;
        }

        public VstsRelease CreateRelease(int releaseDefinitionId)
        {
            ReleaseHttpClient releaseClient = _vssConnection.GetClient<ReleaseHttpClient>();

            var release = CreateRelease(releaseClient, releaseDefinitionId, _configuration.Value.ProjectName);

            return release;
        }

        public static VstsRelease CreateRelease(ReleaseHttpClient releaseClient, int releaseDefinitionId, string projectName, Dictionary<string, ConfigurationVariableValue> overrideVaraibles = null)
        {
            ReleaseStartMetadata releaseStartMetaData = new ReleaseStartMetadata();
            releaseStartMetaData.DefinitionId = releaseDefinitionId;
            releaseStartMetaData.Description = "Release created by das-release-tool";

            if (overrideVaraibles != null)
            {
                // If you want to override varaibles at release create time, 'AllowOverride' on variable should be set while creating RD.
                // You can override environment level variables using releaseStartMetaData.EnvironmentsMetadata.Variables.
                releaseStartMetaData.Variables = overrideVaraibles;
            }

            // Create  a release
            var release =
                releaseClient.CreateReleaseAsync(project: projectName, releaseStartMetadata: releaseStartMetaData).Result;

            var vstsRelease = new VstsRelease
            {
                Id = release.Id,
                ReleaseName = release.Name,
                ReleaseDefininitionId = release.ReleaseDefinitionReference.Id
            };

            return vstsRelease;
        }
    }
}
