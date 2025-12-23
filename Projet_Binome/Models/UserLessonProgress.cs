using System;

namespace Projet_Binome.Models
{
    public class UserLessonProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int LessonId { get; set; }
        public double LastPositionSeconds { get; set; }
        public double DurationSeconds { get; set; }
        public double PercentComplete { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
        public Lesson Lesson { get; set; } = null!;
    }
}
