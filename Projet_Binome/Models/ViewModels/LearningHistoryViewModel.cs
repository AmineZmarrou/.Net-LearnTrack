using System.Collections.Generic;

namespace Projet_Binome.Models.ViewModels
{
    public class LearningHistoryViewModel
    {
        public IList<UserCourseProgress> Courses { get; set; } = new List<UserCourseProgress>();
        public IList<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
        public IList<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
