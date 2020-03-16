using Microsoft.AspNetCore.Mvc;
using SFA.DAS.SelfService.Web.Extensions;
using System;

namespace SFA.DAS.SelfService.Web.Controllers
{
    public class BaseController : Controller
    {
        public RedirectToActionResult RedirectToAction(string actionName, Type controller, object routeValues)
        {
            return base.RedirectToAction(actionName, controller.GetControllerName(), routeValues);
        }
    }
}