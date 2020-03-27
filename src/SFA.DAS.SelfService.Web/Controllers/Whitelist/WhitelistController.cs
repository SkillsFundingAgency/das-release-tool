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
        public IActionResult StartRelease(WhitelistViewModel whitelistViewModel)
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

            var claimName = GetClaimName(this.Request.HttpContext.User.Claims);

            whitelistViewModel.UserId = claimName;

            var whiteListDefinition = _releaseService.GetRelease(WhitelistConstants.ReleaseName);

            if (whiteListDefinition == null)
            {
                _logger.LogError($"Release {WhitelistConstants.ReleaseName} not found!");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Creating release: {whiteListDefinition.ReleaseName}");

            var overrideParameters = new Dictionary<string, string>()
            {
                { WhitelistConstants.IpAddressOverrideKey, whitelistViewModel.IpAddress },
                { WhitelistConstants.UserIdOverrideKey, whitelistViewModel.UserId }
            };

            var release = _releaseService.CreateRelease(whiteListDefinition.Id, overrideParameters);

            TempData.Put("model", new { releaseId = release.Id, releaseDefinitionId = release.ReleaseDefininitionId });

            return RedirectToAction(WhitelistRouteNames.ReleaseCreated);
        }

        [HttpGet("releaseStarted", Name = WhitelistRouteNames.ReleaseCreated)]
        public IActionResult ReleaseCreated()
        {
            return View(new WhitelistReleaseViewModel());
        }

        [HttpGet("releasestatus", Name = WhitelistRouteNames.ReleaseRefresh)]
        public IActionResult ReleaseRefresh()
        {
            var releaseIds = TempData.Peek<WhitelistReleaseViewModel>("model");

            var deploymentStatus = _releaseService.CheckReleaseStatus(releaseIds.releaseDefinitionId, releaseIds.releaseId);

            var whitelistViewModel = new WhitelistReleaseViewModel() { deploymentStatus = deploymentStatus };

            return PartialView("ReleaseCreatedPartial", whitelistViewModel);
        }

        public string GetClaimName(IEnumerable<Claim> claims)
        {
            var claimName = claims.Where(x => x.Type.Contains("nameidentifier")).FirstOrDefault().Value;

            if (String.IsNullOrEmpty(claimName))
            {
                _logger.LogError("Cannot find valid claim name to start whitelist");
                throw new UnauthorizedAccessException();
            }

            return claimName;
        }
    }
}