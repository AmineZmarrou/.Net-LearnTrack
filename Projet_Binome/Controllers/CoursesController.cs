using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Binome.Data;
using Projet_Binome.Models;
using Projet_Binome.Models.Requests;
using Projet_Binome.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projet_Binome.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IDictionary<int, UserLessonProgress> lessonProgress = new Dictionary<int, UserLessonProgress>();
            if (!string.IsNullOrEmpty(userId))
            {
                var progress = await _context.UserCourseProgresses
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == course.Id);

                if (progress == null)
                {
                    progress = new UserCourseProgress
                    {
                        UserId = userId,
                        CourseId = course.Id,
                        PercentComplete = 0,
                        LastAccessed = System.DateTime.UtcNow
                    };
                    _context.UserCourseProgresses.Add(progress);
                }
                else
                {
                    progress.LastAccessed = System.DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                var progressEntries = await _context.UserLessonProgresses
                    .Where(p => p.UserId == userId && p.Lesson.CourseId == course.Id)
                    .ToListAsync();

                lessonProgress = progressEntries.ToDictionary(p => p.LessonId);
            }

            var model = new CourseDetailsViewModel
            {
                Course = course,
                LessonProgress = lessonProgress
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLessonProgress([FromBody] LessonProgressUpdateRequest request)
        {
            if (request == null || request.LessonId <= 0)
            {
                return BadRequest("Invalid lesson progress payload.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == request.LessonId);
            if (lesson == null)
            {
                return NotFound();
            }

            var duration = Math.Max(0, request.DurationSeconds);
            var currentTime = Math.Max(0, request.CurrentTimeSeconds);
            if (duration > 0 && currentTime > duration)
            {
                currentTime = duration;
            }

            var progress = await _context.UserLessonProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LessonId == lesson.Id);

            var computedPercent = duration > 0 ? (currentTime / duration) * 100.0 : 0.0;
            var updatedPercent = progress?.PercentComplete ?? 0.0;
            if (duration > 0)
            {
                updatedPercent = Math.Max(updatedPercent, computedPercent);
            }

            if (progress == null)
            {
                progress = new UserLessonProgress
                {
                    UserId = userId,
                    LessonId = lesson.Id,
                    LastPositionSeconds = currentTime,
                    DurationSeconds = duration,
                    PercentComplete = updatedPercent,
                    LastUpdated = DateTime.UtcNow
                };
                _context.UserLessonProgresses.Add(progress);
            }
            else
            {
                progress.LastPositionSeconds = currentTime;
                progress.DurationSeconds = duration;
                progress.PercentComplete = updatedPercent;
                progress.LastUpdated = DateTime.UtcNow;
            }

            var courseProgress = await _context.UserCourseProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == lesson.CourseId);

            if (courseProgress == null)
            {
                courseProgress = new UserCourseProgress
                {
                    UserId = userId,
                    CourseId = lesson.CourseId,
                    PercentComplete = 0,
                    LastAccessed = DateTime.UtcNow
                };
                _context.UserCourseProgresses.Add(courseProgress);
            }

            var lessonCount = await _context.Lessons.CountAsync(l => l.CourseId == lesson.CourseId);
            if (lessonCount > 0)
            {
                var lessonPercents = await _context.UserLessonProgresses
                    .Where(p => p.UserId == userId && p.Lesson.CourseId == lesson.CourseId)
                    .Select(p => new { p.LessonId, p.PercentComplete })
                    .ToListAsync();

                var totalPercent = lessonPercents.Sum(p => p.PercentComplete);
                var existing = lessonPercents.FirstOrDefault(p => p.LessonId == lesson.Id);
                if (existing != null)
                {
                    totalPercent = totalPercent - existing.PercentComplete + updatedPercent;
                }
                else
                {
                    totalPercent += updatedPercent;
                }

                var averagePercent = totalPercent / lessonCount;
                if (!courseProgress.QuizPassed)
                {
                    courseProgress.PercentComplete = Math.Max(courseProgress.PercentComplete, averagePercent);
                }
                else
                {
                    courseProgress.PercentComplete = 100;
                }
            }

            courseProgress.LastAccessed = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                lessonId = lesson.Id,
                percent = updatedPercent
            });
        }
    }
}
