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
