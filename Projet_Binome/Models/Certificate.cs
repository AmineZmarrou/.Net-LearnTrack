
using System;

namespace Projet_Binome.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public DateTime CompletionDate { get; set; }
    }
}
