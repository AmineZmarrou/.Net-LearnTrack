using System.Collections.Generic;

namespace Projet_Binome.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? YouTubeLink { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public ICollection<UserLessonProgress> UserProgresses { get; set; } = new List<UserLessonProgress>();
    }
}
