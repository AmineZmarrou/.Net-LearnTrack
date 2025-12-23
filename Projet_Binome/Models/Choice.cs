namespace Projet_Binome.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
    }
}
