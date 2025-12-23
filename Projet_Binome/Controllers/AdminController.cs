using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Binome.Data;
using Projet_Binome.Models;
using Projet_Binome.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Binome.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard courses CRUD
        public async Task<IActionResult> Courses()
        {
            var courses = await _context.Courses.OrderBy(c => c.Id).ToListAsync();
            return View(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse([Bind("Title,Description,ImageUrl")] Course course)
        {
            if (!ModelState.IsValid)
            {
                var courses = await _context.Courses.OrderBy(c => c.Id).ToListAsync();
                return View("Courses", courses);
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Courses));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(int id, [Bind("Id,Title,Description,ImageUrl")] Course input)
        {
            if (id != input.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var courses = await _context.Courses.OrderBy(c => c.Id).ToListAsync();
                return View("Courses", courses);
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.Title = input.Title;
            course.Description = input.Description;
            course.ImageUrl = input.ImageUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Courses));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Courses));
        }

        // Users overview
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .Select(u => new AdminUserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    IsAdmin = u.IsAdmin,
                    CreatedAt = u.CreatedAt,
                    CoursesCount = _context.UserCourseProgresses.Count(p => p.UserId == u.Id),
                    QuizAttemptsCount = _context.QuizAttempts.Count(a => a.UserId == u.Id),
                    CertificatesCount = _context.Certificates.Count(c => c.UserId == u.Id)
                })
                .OrderBy(u => u.FullName)
                .ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> UserHistory(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var courses = await _context.UserCourseProgresses
                .Include(p => p.Course)
                .Include(p => p.Certificate)
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.LastAccessed)
                .ToListAsync();

            var attempts = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .ThenInclude(q => q.Course)
                .Where(a => a.UserId == id)
                .OrderByDescending(a => a.TakenAt)
                .ToListAsync();

            var certificates = await _context.Certificates
                .Include(c => c.Course)
                .Where(c => c.UserId == id)
                .OrderByDescending(c => c.CompletionDate)
                .ToListAsync();

            var model = new AdminUserHistoryViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Courses = courses,
                Attempts = attempts,
                Certificates = certificates
            };

            return View(model);
        }
    }
}
