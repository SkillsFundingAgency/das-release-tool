using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;
using Microsoft.VisualStudio.Services.WebApi;
using SFA.DAS.ReleaseTool.Core.Configuration;
using SFA.DAS.ReleaseTool.Infrastructure.IReleases;
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

        public List<ReleaseDefinition> GetReleases()
        {
            var client = _vssConnection.GetClient<ReleaseHttpClient>();

            var release = client.GetReleaseDefinitionsAsync(_configuration.Value.ProjectName, expand: ReleaseDefinitionExpands.Variables).Result;

            return release;
        }

        public ReleaseDefinition GetRelease(string releaseName)
        {
            var client = _vssConnection.GetClient<ReleaseHttpClient>();

            var release = client.GetReleaseDefinitionsAsync(_configuration.Value.ProjectName, releaseName, expand: ReleaseDefinitionExpands.Variables).Result;

            return release.FirstOrDefault();
        }

        public DeploymentStatus CheckReleaseStatus(int releaseDefinitionId, int releaseId)
        {
            ReleaseHttpClient releaseClient = _vssConnection.GetClient<ReleaseHttpClient>();

            List<Deployment> deployments = releaseClient.GetDeploymentsAsync(project: _configuration.Value.ProjectName, definitionId: releaseDefinitionId, top: 5).Result;

            var deployment = deployments.Single(r => r.Id == releaseId);

            return deployment.DeploymentStatus;
        }

        public Release CreateRelease(int releaseDefinitionId, string ipAddress)
        {
            ReleaseHttpClient releaseClient = _vssConnection.GetClient<ReleaseHttpClient>();

            Dictionary<string, ConfigurationVariableValue> overRideReleaseLevelVariables = new Dictionary<string, ConfigurationVariableValue>();
            ConfigurationVariableValue ip = new ConfigurationVariableValue();

            ip.Value = ipAddress;
            ip.IsSecret = false;
            overRideReleaseLevelVariables.Add(ReleaseConstants.IpAddressKey, ip);

            Release release = CreateRelease(releaseClient, releaseDefinitionId, _configuration.Value.ProjectName, overRideReleaseLevelVariables);

            return release;
        }

        public static Release CreateRelease(ReleaseHttpClient releaseClient, int releaseDefinitionId, string projectName, Dictionary<string, ConfigurationVariableValue> overrideVaraibles = null)
        {
            ReleaseStartMetadata releaseStartMetaData = new ReleaseStartMetadata();
            releaseStartMetaData.DefinitionId = releaseDefinitionId;
            releaseStartMetaData.Description = "Creating Sample release";

            if (overrideVaraibles != null)
            {
                // If you want to override varaibles at release create time, 'AllowOverride' on variable should be set while creating RD.
                // You can override environment level variables using releaseStartMetaData.EnvironmentsMetadata.Variables.
                releaseStartMetaData.Variables = overrideVaraibles;
            }

            // Create  a release
            Release release =
                releaseClient.CreateReleaseAsync(project: projectName, releaseStartMetadata: releaseStartMetaData).Result;
            return release;
        }
    }
}
