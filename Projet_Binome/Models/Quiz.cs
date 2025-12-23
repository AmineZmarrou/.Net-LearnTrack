using System.Collections.Generic;

namespace Projet_Binome.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
