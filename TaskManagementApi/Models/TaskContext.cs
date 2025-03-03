using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class TaskContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    public new DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<TaskLabel> TaskLabels { get; set; }
    public DbSet<TaskAttachment> TaskAttachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User - TaskItem (One-to-Many)
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // TaskItem - Category (One-to-Many)
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // TaskItem - TaskComment (One-to-Many)
        modelBuilder.Entity<TaskComment>()
            .HasOne(tc => tc.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // TaskItem - TaskAttachment (One-to-Many)
        modelBuilder.Entity<TaskAttachment>()
            .HasOne(ta => ta.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // User - TaskComment (One-to-Many)
        modelBuilder.Entity<TaskComment>()
            .HasOne(tc => tc.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(tc => tc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // TaskItem - Label (Many-to-Many via TaskLabel)
        modelBuilder.Entity<TaskLabel>()
            .HasKey(tl => new { tl.TaskId, tl.LabelId });

        modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Task)
            .WithMany(t => t.TaskLabels)
            .HasForeignKey(tl => tl.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Label)
            .WithMany(l => l.TaskLabels)
            .HasForeignKey(tl => tl.LabelId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique constraints
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<Label>()
            .HasIndex(l => l.Name)
            .IsUnique();

        // Default values
        modelBuilder.Entity<TaskItem>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<TaskComment>()
            .Property(tc => tc.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<TaskAttachment>()
            .Property(ta => ta.UploadedAt)
            .HasDefaultValueSql("GETDATE()");

        // Seed data
        List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

        modelBuilder.Entity<IdentityRole>().HasData(roles);

        List<Category> categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Personal"
                },
                new Category
                {
                    Id = 2,
                    Name = "Work"
                },
                new Category
                {
                    Id = 3,
                    Name = "Shopping"
                },
            };

        modelBuilder.Entity<Category>().HasData(categories);

        List<Label> labels = new List<Label>
            {
                new Label
                {
                    Id = 1,
                    Name = "Urgent"
                },
                new Label
                {
                    Id = 2,
                    Name = "Important"
                },
                new Label
                {
                    Id = 3,
                    Name = "Home"
                },
                new Label
                {
                    Id = 4,
                    Name = "Office"
                },
            };

        modelBuilder.Entity<Label>().HasData(labels);

        //var date = new DateTime(2021, 10, 1);

        //List<TaskItem> tasks = new List<TaskItem>
        //    {
        //        new TaskItem
        //        {
        //            Id = 1,
        //            Title = "Learn ASP.NET Core",
        //            Description = "Study the basics of ASP.NET Core framework and its components",
        //            IsCompleted = false,
        //            UserId = "1",
        //            CategoryId = 2,
        //            CreatedAt = date
        //        },
        //        new TaskItem
        //        {
        //            Id = 2,
        //            Title = "Create a new project",
        //            Description = "Set up a new ASP.NET Core project using Visual Studio or Visual Studio Code",
        //            IsCompleted = false,
        //            UserId = "1",
        //            CategoryId = 2,
        //            CreatedAt = date

        //        },
        //        new TaskItem
        //        {
        //            Id = 3,
        //            Title = "Add a new feature",
        //            Description = "Implement a new feature based on project requirements",
        //            IsCompleted = false,
        //            UserId = "1",
        //            CategoryId = 2,
        //            CreatedAt = date

        //        },
        //        new TaskItem
        //        {
        //            Id = 4,
        //            Title = "Deploy the app",
        //            Description = "Deploy the application to a hosting service like Azure or AWS",
        //            IsCompleted = false,
        //            UserId = "1",
        //            CategoryId = 2,
        //            CreatedAt = date

        //        },
        //    };

        //modelBuilder.Entity<TaskItem>().HasData(tasks);

        //List<TaskLabel> taskLabels = new List<TaskLabel>
        //    {
        //        new TaskLabel
        //        {
        //            TaskId = 1,
        //            LabelId = 1
        //        },
        //        new TaskLabel
        //        {
        //            TaskId = 1,
        //            LabelId = 2
        //        },
        //        new TaskLabel
        //        {
        //            TaskId = 2,
        //            LabelId = 2
        //        },
        //        new TaskLabel
        //        {
        //            TaskId = 3,
        //            LabelId = 2
        //        },
        //        new TaskLabel
        //        {
        //            TaskId = 4,
        //            LabelId = 2
        //        },
        //    };

        //modelBuilder.Entity<TaskLabel>().HasData(taskLabels);

        //List<TaskComment> comments = new List<TaskComment>
        //    {
        //        new TaskComment
        //        {
        //            Id = 1,
        //            TaskId = 1,
        //            UserId = "1",
        //            Content = "This is a great task!",
        //            CreatedAt = new DateTime(2021, 10, 1)
        //        },
        //        new TaskComment
        //        {
        //            Id = 2,
        //            TaskId = 1,
        //            UserId = "1",
        //            Content = "I'm making progress on this task.",
        //            CreatedAt = new DateTime(2021, 10, 2)
        //        },
        //        new TaskComment
        //        {
        //            Id = 3,
        //            TaskId = 2,
        //            UserId = "1",
        //            Content = "I'm excited to start this project.",
        //            CreatedAt = new DateTime(2021, 10, 1)
        //        },
        //    };

        //modelBuilder.Entity<TaskComment>().HasData(comments);

        //var passwordHasher = new PasswordHasher<User>();

        //List<User> users = new List<User>
        //{
        //    new User
        //    {
        //        Id = "1",
        //        UserName = "admin",
        //        NormalizedUserName = "ADMIN",
        //        Email = "admin@example.com",
        //        NormalizedEmail = "ADMIN@EXAMPLE.COM",
        //        EmailConfirmed = true,
        //        SecurityStamp = "STATIC-SECURITY-STAMP-ADMIN"
        //    }
        //};

        //users.ForEach(u => u.PasswordHash = passwordHasher.HashPassword(u, "Admin123!"));

        //modelBuilder.Entity<User>().HasData(users);
    }
}
