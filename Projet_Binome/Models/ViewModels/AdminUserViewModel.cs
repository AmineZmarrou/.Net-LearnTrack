using System;
using System.Collections.Generic;

namespace Projet_Binome.Models.ViewModels
{
    public class AdminUserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CoursesCount { get; set; }
        public int QuizAttemptsCount { get; set; }
        public int CertificatesCount { get; set; }
    }

    public class AdminUserHistoryViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<UserCourseProgress> Courses { get; set; } = new List<UserCourseProgress>();
        public IList<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
        public IList<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
