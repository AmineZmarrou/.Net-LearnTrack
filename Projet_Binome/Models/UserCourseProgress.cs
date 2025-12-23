using System;

namespace Projet_Binome.Models
{
    public class UserCourseProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int CourseId { get; set; }
        public double PercentComplete { get; set; }
        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedOn { get; set; }
        public bool QuizPassed { get; set; }
        public int? CertificateId { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Certificate? Certificate { get; set; }
    }
}
