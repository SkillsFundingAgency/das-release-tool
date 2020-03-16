using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ReleaseTool.Core.Configuration;
using SFA.DAS.ReleaseTool.Core.IServices;
using SFA.DAS.ReleaseTool.Web.Configuration;
using SFA.DAS.ReleaseTool.Web.Extensions;
using SFA.DAS.ReleaseTool.Web.Models;
using SFA.DAS.ReleaseTool.Web.Models.Whitelist;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ReleaseTool.Web.Controllers.Whitelist
{
    [Route("/whitelist")]
    public class WhitelistController : BaseController
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
        public IActionResult StartRelease(IndexViewModel indexViewModel)
        {
            if (String.IsNullOrEmpty(indexViewModel.IpAddress))
            {
                _logger.LogError("IP Address cannot be null");
                return new BadRequestResult();
            }

            var whiteListDefinition = _releaseService.GetRelease(WhitelistConstants.ReleaseName);

            if (whiteListDefinition == null)
            {
                _logger.LogError($"Release {WhitelistConstants.ReleaseName} not found!");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Creating release: {whiteListDefinition.ReleaseName}");

            var overrideParameters = new Dictionary<string, string>()
            {
                { WhitelistConstants.OverrideKey, indexViewModel.IpAddress }
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

            return PartialView("ReleaseCreatedPartial",whitelistViewModel);
        }
    }
}
