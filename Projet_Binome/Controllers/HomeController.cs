using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projet_Binome.Data;
using Projet_Binome.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Projet_Binome.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CourseCount = await _context.Courses.CountAsync();
            ViewBag.LessonCount = await _context.Lessons.CountAsync();

            var totalAttempts = await _context.QuizAttempts.CountAsync();
            var passedAttempts = await _context.QuizAttempts.CountAsync(a => a.Passed);
            var passRate = totalAttempts > 0 ? (int)System.Math.Round((double)passedAttempts / totalAttempts * 100) : 0;

            ViewBag.QuizPassRate = passRate;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
