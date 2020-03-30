using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.SelfService.Core.IServices;

namespace SFA.DAS.SelfService.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReleaseService _releaseService;

        public HomeController(ILogger<HomeController> logger, IReleaseService releaseService)
        {
            _logger = logger;
            _releaseService = releaseService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return null;
        }
    }
}