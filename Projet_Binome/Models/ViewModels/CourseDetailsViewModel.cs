using System.Collections.Generic;

namespace Projet_Binome.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; } = null!;
        public IDictionary<int, UserLessonProgress> LessonProgress { get; set; } = new Dictionary<int, UserLessonProgress>();
    }
}
