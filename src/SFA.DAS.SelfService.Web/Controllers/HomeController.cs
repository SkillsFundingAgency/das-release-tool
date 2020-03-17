using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.SelfService.Core.IServices;
using SFA.DAS.SelfService.Web.Models;
using System.Diagnostics;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
        }
    }
}