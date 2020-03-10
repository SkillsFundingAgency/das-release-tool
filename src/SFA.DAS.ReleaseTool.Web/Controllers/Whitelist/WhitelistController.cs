using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ReleaseTool.Core.IServices;
using SFA.DAS.ReleaseTool.Web.Extensions;
using SFA.DAS.ReleaseTool.Web.Models.Whitelist;

namespace SFA.DAS.ReleaseTool.Web.Controllers.Whitelist
{
    public class WhitelistController : BaseController
    {
        private readonly IReleaseService _releaseService;

        public WhitelistController(IReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        [HttpGet()]
        public IActionResult ReleaseStarted()
        {
            var releaseIds = TempData.Peek<WhitelistViewModel>("model");

            var deploymentStatus = _releaseService.CheckReleaseStatus(releaseIds.releaseDefinitionId, releaseIds.releaseId);

            var whitelistViewModel = new WhitelistViewModel() { deploymentStatus = deploymentStatus };

            return View("WhitelistReleaseCreated", whitelistViewModel);
        }

        [HttpGet()]
        public IActionResult ReleaseRefresh(int release, int releaseDefinition)
        {
            var releaseIds = TempData.Peek<WhitelistViewModel>("model");

            var deploymentStatus = _releaseService.CheckReleaseStatus(releaseIds.releaseDefinitionId, releaseIds.releaseId);

            var whitelistViewModel = new WhitelistViewModel() { deploymentStatus = deploymentStatus };

            return PartialView("WhitelistPartial", whitelistViewModel);
        }
    }
}
