using System;
using System.Collections.Generic;

namespace Projet_Binome.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsAdmin { get; set; } = false;

        public ICollection<UserCourseProgress> CourseProgress { get; set; } = new List<UserCourseProgress>();
        public ICollection<UserLessonProgress> LessonProgress { get; set; } = new List<UserLessonProgress>();
        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
