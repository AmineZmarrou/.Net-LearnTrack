using System;

namespace Projet_Binome.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int QuizId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
        public Quiz Quiz { get; set; } = null!;
    }
}
