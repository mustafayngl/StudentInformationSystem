using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
