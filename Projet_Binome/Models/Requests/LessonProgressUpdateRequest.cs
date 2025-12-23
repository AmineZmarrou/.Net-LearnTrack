namespace Projet_Binome.Models.Requests
{
    public class LessonProgressUpdateRequest
    {
        public int LessonId { get; set; }
        public double CurrentTimeSeconds { get; set; }
        public double DurationSeconds { get; set; }
    }
}
