using System.Collections.Generic;

namespace Projet_Binome.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();
    }
}
