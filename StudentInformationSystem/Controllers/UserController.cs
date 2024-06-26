﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly SchoolDbContext _context;

        public UserController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index(string search)
        {
            var users = _context.Users.AsQueryable(); // Tüm kullanıcıları sorguya hazırla

            if (!String.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.Username.Contains(search)); // Kullanıcı adına göre arama yap
            }

            return View(await users.ToListAsync());
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Role,IdentityNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Role,IdentityNumber")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.IdentityNumber == model.IdentityNumber);
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("", "Invalid username, password, or identity number.");
                return View(user);
            }

            // Authentication successful
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role), // Kullanıcının rolü
                // İhtiyaç duyulan diğer isteğe bağlı talepler buraya eklenebilir
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal); // Kullanıcıyı kimlik doğrulama mekanizması aracılığıyla giriş yapmış olarak işaretle

            // Rollerle birlikte yönlendirme yap
            switch (user.Role)
            {
                case "admin":
                    return RedirectToAction("Index", "Admin"); // Admin paneline yönlendir
                case "teacher":
                    return RedirectToAction("Index", "TeacherMain"); // Öğretmen paneline yönlendir
                case "student":
                    return RedirectToAction("DetailsByIdentityNumber", "StudentMain", new { identityNumber = user.IdentityNumber }); // Öğrenci detaylarına yönlendir
                default:
                    ModelState.AddModelError("", "Unknown role.");
                    return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Kullanıcı oturumu kapatılıyor
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "User");
        }

        private bool UserExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }

    }
}

