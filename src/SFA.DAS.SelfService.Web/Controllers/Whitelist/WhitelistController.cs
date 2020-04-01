using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.SelfService.Core.Configuration;
using SFA.DAS.SelfService.Core.IServices;
using SFA.DAS.SelfService.Web.Configuration;
using SFA.DAS.SelfService.Web.Extensions;
using SFA.DAS.SelfService.Web.Models;
using SFA.DAS.SelfService.Web.Models.Whitelist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SFA.DAS.SelfService.Web.Controllers.Whitelist
{
    [Route("/whitelist")]
    public class WhitelistController : BaseController<WhitelistController>
    {
        private readonly IReleaseService _releaseService;
        private readonly ILogger _logger;

        public WhitelistController(ILogger<WhitelistController> logger, IReleaseService releaseService)
        {
            _releaseService = releaseService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var whitelistViewModel = new WhitelistViewModel();

            return View(whitelistViewModel);
        }

        [HttpPost("start", Name = WhitelistRouteNames.CreateRelease)]
        public async Task<IActionResult> StartRelease(WhitelistViewModel whitelistViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State");
                return View("Index", whitelistViewModel);
            }
            if (String.IsNullOrEmpty(whitelistViewModel.IpAddress))
            {
                _logger.LogError("IP Address cannot be null");
                return new BadRequestResult();
            }

            var whiteListDefinition = await _releaseService.GetReleaseAsync(WhitelistConstants.ReleaseDefinitionId);

            if (whiteListDefinition == null)
            {
                _logger.LogError($"Release with Id {WhitelistConstants.ReleaseDefinitionId} not found!");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Creating release: {whiteListDefinition.ReleaseName}");

            var overrideParameters = SetupOverrideVariables(whitelistViewModel.IpAddress, this.Request.HttpContext.User.Claims);

            var release =  await _releaseService.CreateRelease(whiteListDefinition.Id, overrideParameters);

            TempData.Put("model", new { releaseId = release.Id, releaseDefinitionId = release.ReleaseDefininitionId });

            foreach (var environmentDefinitionId in whitelistViewModel.SelectedEnvironmentIds)
            {
                var environmentId = release.ReleaseEnvironments.Single(x => x.DefinitionId == environmentDefinitionId).EnvironmentReleaseId;

                await _releaseService.StartEnvironmentDeployment(release, environmentId);
            }

            return RedirectToAction(WhitelistRouteNames.ReleaseCreated);
        }

        [HttpGet("releaseStarted", Name = WhitelistRouteNames.ReleaseCreated)]
        public async Task<IActionResult> ReleaseCreated()
        {
            var whitelistViewModel = await GetDeploymentStatus();

            return View(whitelistViewModel);
        }

        [HttpGet("releaseStatus", Name = WhitelistRouteNames.ReleaseRefresh)]
        public async Task<IActionResult> ReleaseRefresh()
        {
            var whitelistViewModel = await GetDeploymentStatus();

            return PartialView("ReleaseCreatedPartial", whitelistViewModel);
        }

        private async Task<WhitelistReleaseViewModel> GetDeploymentStatus()
        {
            var releaseIds = TempData.Peek<WhitelistReleaseViewModel>("model");

            var deploymentStatus = await _releaseService.CheckReleaseStatus(releaseIds.releaseDefinitionId, releaseIds.releaseId);

            return new WhitelistReleaseViewModel() { deploymentStatus = deploymentStatus };
        }

        private Dictionary<string, string> SetupOverrideVariables(string ipAddress, IEnumerable<Claim> claims)
        {
            var userId = claims.Where(x => x.Type.Contains("nameidentifier")).FirstOrDefault().Value;

            if (String.IsNullOrEmpty(userId))
            {
                _logger.LogError("Cannot find a valid userId to start whitelist");
                throw new UnauthorizedAccessException();
            }

            var nickname = claims.Where(x => x.Type.Contains("nickname")).FirstOrDefault().Value;

            if (String.IsNullOrEmpty(nickname))
            {
                _logger.LogError("Cannot find a valid nickname to start whitelist");
                throw new UnauthorizedAccessException();
            }

            var overrideParameters = new Dictionary<string, string>()
            {
                { WhitelistConstants.IpAddressOverrideKey, ipAddress},
                { WhitelistConstants.UserIdOverrideKey, userId},
                { WhitelistConstants.NameOverrideKey, nickname}
            };

            return overrideParameters;
        }
    }
}