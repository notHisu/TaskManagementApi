using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Repositories
{
    public class TaskRepository : IGenericRepository<TaskItem>
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public void Add(TaskItem entity)
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

            _context.TaskItems.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id, int? secondId = null)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _context.TaskItems.Remove(task);
            _context.SaveChanges();
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        public TaskItem? GetById(int id, int? secondId = null)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            return task;
        }

        public void Update(TaskItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Task cannot be null");
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
