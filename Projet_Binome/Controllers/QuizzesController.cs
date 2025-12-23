using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Binome.Data;
using Projet_Binome.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projet_Binome.Controllers
{
    [Authorize]
    public class QuizzesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizzesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TakeQuiz(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            ViewBag.UserName = User.Identity?.Name ?? "Utilisateur";
            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitQuiz(Microsoft.AspNetCore.Http.IFormCollection form)
        {
            if (!int.TryParse(form["quizId"], out var quizId))
            {
                return BadRequest("Invalid quiz id.");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return Challenge();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
            {
                return NotFound();
            }

            int score = 0;
            foreach (var question in quiz.Questions)
            {
                if (form.TryGetValue($"question-{question.Id}", out var value))
                {
                    if (int.TryParse(value, out var choiceId))
                    {
                        var choice = question.Choices.FirstOrDefault(c => c.Id == choiceId);
                        if (choice != null && choice.IsCorrect)
                        {
                            score++;
                        }
                    }
                }
            }

            int totalQuestions = quiz.Questions.Count;
            double percentage = totalQuestions == 0 ? 0 : (double)score / totalQuestions;
            bool passed = percentage >= 0.7;

            var attempt = new QuizAttempt
            {
                QuizId = quiz.Id,
                UserId = user.Id,
                Score = score,
                TotalQuestions = totalQuestions,
                Percentage = percentage,
                Passed = passed
            };
            _context.QuizAttempts.Add(attempt);

            var progress = await _context.UserCourseProgresses
                .FirstOrDefaultAsync(p => p.UserId == user.Id && p.CourseId == quiz.CourseId);
            if (progress == null)
            {
                progress = new UserCourseProgress
                {
                    UserId = user.Id,
                    CourseId = quiz.CourseId,
                    PercentComplete = passed ? 100 : 60,
                    QuizPassed = passed,
                    CompletedOn = passed ? System.DateTime.UtcNow : null,
                    LastAccessed = System.DateTime.UtcNow
                };
                _context.UserCourseProgresses.Add(progress);
            }
            else
            {
                progress.LastAccessed = System.DateTime.UtcNow;
                progress.QuizPassed = passed;
                if (passed)
                {
                    progress.PercentComplete = 100;
                    progress.CompletedOn ??= System.DateTime.UtcNow;
                }
                else if (progress.PercentComplete < 60)
                {
                    progress.PercentComplete = 60;
                }
            }

            if (passed)
            {
                var certificate = await _context.Certificates
                    .FirstOrDefaultAsync(c => c.CourseId == quiz.CourseId && c.UserId == user.Id);

                if (certificate == null)
                {
                    certificate = new Certificate
                    {
                        UserName = user.FullName,
                        UserId = user.Id,
                        CourseId = quiz.CourseId,
                        CompletionDate = System.DateTime.Now
                    };
                    _context.Certificates.Add(certificate);
                }

                await _context.SaveChangesAsync();

                progress.CertificateId = certificate.Id;
                await _context.SaveChangesAsync();

                return RedirectToAction("Congratulations", new
                {
                    certificateId = certificate.Id,
                    score,
                    total = totalQuestions,
                    percent = (int)System.Math.Round(percentage * 100, 0)
                });
            }
            else
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("TryAgain", new
                {
                    quizId = quiz.Id,
                    score,
                    total = totalQuestions,
                    percent = (int)System.Math.Round(percentage * 100, 0)
                });
            }
        }

        public async Task<IActionResult> Congratulations(int certificateId, int score, int total, int percent)
        {
            var certificate = await _context.Certificates
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == certificateId);

            if (certificate == null)
            {
                return NotFound();
            }

            ViewBag.Score = score;
            ViewBag.Total = total;
            ViewBag.Percent = percent;
            return View(certificate);
        }

        public IActionResult TryAgain(int quizId, int score, int total, int percent)
        {
            ViewBag.QuizId = quizId;
            ViewBag.Score = score;
            ViewBag.Total = total;
            ViewBag.Percent = percent;
            return View();
        }
    }
}
