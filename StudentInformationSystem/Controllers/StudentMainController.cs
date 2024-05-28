using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    public class StudentMainController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentMainController(SchoolDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Student/DetailsByIdentityNumber/12345678
        public async Task<IActionResult> DetailsByIdentityNumber(string identityNumber)
        {
            if (string.IsNullOrEmpty(identityNumber))
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.IdentityNumber == identityNumber);
            if (student == null)
            {
                return NotFound();
            }

            return View("Index", student); // Details view'ını kullanarak öğrenci bilgilerini gösteriyoruz
        }

        public IActionResult LessonPlan()
        {
            return View();
        }

    }
}
