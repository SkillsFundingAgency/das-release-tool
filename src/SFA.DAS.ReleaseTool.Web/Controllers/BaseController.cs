using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ReleaseTool.Web.Extensions;
using System;

namespace SFA.DAS.ReleaseTool.Web.Controllers
{
    public class BaseController : Controller
    {
        public RedirectToActionResult RedirectToAction(string actionName, Type controller, object routeValues)
        {
            return base.RedirectToAction(actionName, controller.GetControllerName(), routeValues);
        }
    }
}
