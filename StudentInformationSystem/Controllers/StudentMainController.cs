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

            var announcements = await _context.Announcements.ToListAsync();
            ViewBag.Announcements = announcements;

            return View("MyDetails", student); // Details view'ını kullanarak öğrenci bilgilerini gösteriyoruz
        }

        public IActionResult LessonPlan()
        {
            return View();
        }
        public IActionResult AcademicCalendar()
        {
            return View();
        }

        // Öğrenci kimlik numarasına göre detaylarını ve notlarını gösteren action
        //[Authorize]
        public async Task<IActionResult> MyGrades(string identityNumber)
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
            
            // Öğrenciye ait notları çekmek
            var grades = await _context.Grades
                .Where(g => g.StudentId == student.Id)
                .ToListAsync();
            
            foreach (var grade in grades)
            {
                // Ders adını çekmek için ilgili dersin koduna göre ilgili dersi bulun
                var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Code == grade.Code);

                // Eğer ders bulunamazsa, uyarı verebilirsiniz veya herhangi bir işlem yapabilirsiniz
                if (lesson != null)
                {
                    // Ders adını not nesnesine atayın
                    grade.LessonName = lesson.Name;
                }
            }
            
            // Görünüme öğrenci ve notları bir arada gönder
            ViewBag.Student = student;
            ViewBag.Grades = grades;
            
            return View();
        }
    }
}
