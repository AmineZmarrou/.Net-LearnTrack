using Microsoft.EntityFrameworkCore;
using Projet_Binome.Models;

namespace Projet_Binome.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<UserCourseProgress> UserCourseProgresses { get; set; }
        public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Quiz)
                .WithOne(q => q.Course)
                .HasForeignKey<Quiz>(q => q.CourseId);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.NormalizedEmail)
                .IsUnique();

            modelBuilder.Entity<UserCourseProgress>()
                .HasOne(p => p.User)
                .WithMany(u => u.CourseProgress)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCourseProgress>()
                .HasOne(p => p.Course)
                .WithMany()
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCourseProgress>()
                .HasOne(p => p.Certificate)
                .WithOne()
                .HasForeignKey<UserCourseProgress>(p => p.CertificateId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserCourseProgress>()
                .HasIndex(p => new { p.UserId, p.CourseId })
                .IsUnique();

            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(p => p.User)
                .WithMany(u => u.LessonProgress)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(p => p.Lesson)
                .WithMany(l => l.UserProgresses)
                .HasForeignKey(p => p.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLessonProgress>()
                .HasIndex(p => new { p.UserId, p.LessonId })
                .IsUnique();

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(a => a.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(a => a.Quiz)
                .WithMany()
                .HasForeignKey(a => a.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certificates)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
