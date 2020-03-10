using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ReleaseTool.Core.Configuration;
using SFA.DAS.ReleaseTool.Core.IServices;
using SFA.DAS.ReleaseTool.Web.Extensions;
using SFA.DAS.ReleaseTool.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace SFA.DAS.ReleaseTool.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly IReleaseService releaseService;

        public HomeController(ILogger<HomeController> _logger, IReleaseService _releaseService)
        {
            logger = _logger;
            releaseService = _releaseService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            return View("Index", indexViewModel);
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public IActionResult StartRelease(IndexViewModel indexViewModel)
        {
            if (String.IsNullOrEmpty(indexViewModel.IpAddress))
            {
                logger.LogError("IP Address cannot be null");
                return new BadRequestResult();
            }

            var whiteListDefinition = releaseService.GetRelease(ReleaseConstants.ReleaseName);

            if (whiteListDefinition == null)
            {
                logger.LogError($"Release {ReleaseConstants.ReleaseName} not found!");
                return new NotFoundResult();
            }

            var ipAddressKey = whiteListDefinition.Variables.Where(v => v.Value.AllowOverride == true).First();

            logger.LogInformation($"Creating release: {whiteListDefinition.Name}");

            var release = releaseService.CreateRelease(whiteListDefinition.Id, indexViewModel.IpAddress);

            TempData.Put("model", new { releaseId = release.Id, releaseDefinitionId = release.ReleaseDefinitionReference.Id });

            return RedirectToAction("ReleaseStarted", "Whitelist");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
        }
    }
}
