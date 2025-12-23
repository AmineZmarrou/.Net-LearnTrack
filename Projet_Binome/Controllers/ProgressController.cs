using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Binome.Data;
using Projet_Binome.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projet_Binome.Controllers
{
    [Authorize]
    public class ProgressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            var courses = await _context.UserCourseProgresses
                .Include(p => p.Course)
                .Include(p => p.Certificate)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.LastAccessed)
                .ToListAsync();

            var attempts = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .ThenInclude(q => q.Course)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.TakenAt)
                .ToListAsync();

            var certificates = await _context.Certificates
                .Include(c => c.Course)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CompletionDate)
                .ToListAsync();

            var model = new LearningHistoryViewModel
            {
                Courses = courses,
                Attempts = attempts,
                Certificates = certificates
            };

            return View(model);
        }
    }
}
