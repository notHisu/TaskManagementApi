using Microsoft.EntityFrameworkCore;
using System;

namespace TaskManagementApi.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskLabel> TaskLabels { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskLabel>()
                .HasKey(tl => new { tl.TaskId, tl.LabelId });

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "john_doe", Email = "john@example.com", PasswordHash = "hashed_password", PasswordSalt = "salt" },
                new User { Id = 2, Username = "jane_doe", Email = "jane@example.com", PasswordHash = "hashed_password", PasswordSalt = "salt" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Work", Description = "Work related tasks" },
                new Category { Id = 2, Name = "Personal", Description = "Personal tasks" }
            );

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Learn ASP.NET Core", Description = "Study the basics of ASP.NET Core framework and its components", IsCompleted = false, UserId = 1, CategoryId = 1, CreatedAt = new DateTime(2023, 10, 1) },
                new TaskItem { Id = 2, Title = "Create a new project", Description = "Set up a new ASP.NET Core project using Visual Studio or Visual Studio Code", IsCompleted = false, UserId = 2, CategoryId = 2, CreatedAt = new DateTime(2023, 10, 1) }
            );

            modelBuilder.Entity<Label>().HasData(
                new Label { Id = 1, Name = "Urgent" },
                new Label { Id = 2, Name = "Low Priority" }
            );

            modelBuilder.Entity<TaskLabel>().HasData(
                new TaskLabel { TaskId = 1, LabelId = 1 },
                new TaskLabel { TaskId = 2, LabelId = 2 }
            );

            modelBuilder.Entity<TaskComment>().HasData(
                new TaskComment { Id = 1, TaskId = 1, UserId = 1, Content = "This is a comment on task 1", CreatedAt = new DateTime(2023, 10, 1) },
                new TaskComment { Id = 2, TaskId = 2, UserId = 2, Content = "This is a comment on task 2", CreatedAt = new DateTime(2023, 10, 1) }
            );
        }
    }
}
