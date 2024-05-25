using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Controllers
{
    public class TeacherMainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
