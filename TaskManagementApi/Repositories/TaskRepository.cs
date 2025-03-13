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

        public async Task<TaskItem> AddAsync(TaskItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Task cannot be null");
            }

            await _context.TaskItems.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems
                .Include(t => t.User)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            var task = await _context.TaskItems
                .Include(t => t.User)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            return task;
        }

        public async Task UpdateAsync(int id, TaskItem entity)
        {
            var existingTask = await _context.TaskItems.FindAsync(id);

            if (existingTask == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _context.Entry(existingTask).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
