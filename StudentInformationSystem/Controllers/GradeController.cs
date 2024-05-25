using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    public class GradeController : Controller
    {
        private readonly SchoolDbContext _context;

        public GradeController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grade.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        public IActionResult Create()
        {
            ViewBag.StudentList = new SelectList(_context.Students, "StudentNumber", "StudentNumber");
            ViewBag.LessonList = new SelectList(_context.Lessons, "Code", "Code");
            return View();
        }

        // POST: Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentNumber,Code,GradeValue,Comments")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                // Find the corresponding student and lesson based on the selected values
                var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == grade.StudentNumber);
                var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Code == grade.Code);

                // If either student or lesson is not found, return NotFound
                if (student == null || lesson == null)
                {
                    return NotFound();
                }

                // Assign the IDs of the student and lesson to the grade object
                grade.StudentId = student.Id;
                grade.LessonId = lesson.Id;

                // Add the grade to the context and save changes
                _context.Add(grade);
                await _context.SaveChangesAsync();

                // Redirect to the index action
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, repopulate the dropdown lists and return the view
            ViewBag.StudentList = new SelectList(_context.Students, "StudentNumber", "StudentNumber", grade.StudentNumber);
            ViewBag.LessonList = new SelectList(_context.Lessons, "Code", "Code", grade.Code);
            return View(grade);
        }
        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentNumber,Code,GradeValue")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grade.FindAsync(id);
            if (grade != null)
            {
                _context.Grade.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.Id == id);
        }
    }
}
