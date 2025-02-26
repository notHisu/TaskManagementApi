using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Repositories
{
    public class TaskRepository : ITaskRepository<TaskResponseDto>
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public TaskResponseDto MapToResponseDto(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                UserId = task.UserId,
                CategoryId = task.CategoryId,
                CreatedAt = task.CreatedAt
            };
        }

        public List<TaskResponseDto> MapToResponseDtos(IEnumerable<TaskItem> tasks)
        {
            return tasks.Select(MapToResponseDto).ToList();
        }

        public void Add(TaskCreateDto entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Task cannot be null");
            }

            var userExists = _context.Users.Any(u => u.Id == entity.UserId);
            var categoryExists = _context.Categories.Any(c => c.Id == entity.CategoryId);

            if (!userExists)
            {
                throw new InvalidOperationException("User not found");
            }

            if (!categoryExists)
            {
                throw new InvalidOperationException("Category not found");
            }

            var newTask = new TaskItem
            {
                Title = entity.Title,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted,
                UserId = entity.UserId,
                CategoryId = entity.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            _context.TaskItems.Add(newTask);
            _context.SaveChanges();
        }

        public IEnumerable<TaskResponseDto> GetAll()
        {
            var tasks = _context.TaskItems
                .Include(t => t.User)
                .Include(t => t.Category)
                .ToList();
            return MapToResponseDtos(tasks);
        }

        public TaskResponseDto? GetById(int id)
        {
            var task = _context.TaskItems
                .Include(t => t.User)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            return MapToResponseDto(task);
        }

        public void Update(int id, TaskUpdateDto entity)
        {
            var task = _context.TaskItems.Find(id);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Task cannot be null");
            }

            if (entity.Title != null)
            {
                task.Title = entity.Title;
            }
            if (entity.Description != null) {
                task.Description = entity.Description;
            }
            if (entity.IsCompleted != null)
            {
                task.IsCompleted = entity.IsCompleted.Value;
            }
            if (entity.CategoryId != null)
            {
                task.CategoryId = entity.CategoryId.Value;
            }

            _context.TaskItems.Update(task);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _context.TaskItems.Remove(task);
            _context.SaveChanges();
        }
    }
}
