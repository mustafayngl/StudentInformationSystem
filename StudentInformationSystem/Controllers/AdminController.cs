using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Layout"] = "~/Views/Shared/_AdminLayout.cshtml";
            return View();
        }
    }

}
