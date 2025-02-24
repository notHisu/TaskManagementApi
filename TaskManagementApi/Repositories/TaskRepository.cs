using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

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
            if (entity == null) {
                throw new ArgumentNullException("Task cannot be null");
            }

            _context.TaskItems.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = _context.TaskItems.FirstOrDefault(x => x.Id == id);

            if(task != null)
            {
                _context.TaskItems.Remove(task);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Task not found");
            }
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        public TaskItem? GetById(int id)
        {
            var task = _context.TaskItems.FirstOrDefault(x => x.Id == id);

            if (task != null)
            {
                return task; 
            }
            else
            {
                throw new InvalidOperationException("Task not found");
            }
        }

        public void Update(TaskItem entity)
        {
            var existingEntity = _context.TaskItems.Find(entity.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            else
            {
                throw new InvalidOperationException("Task not found");
            }

            _context.TaskItems.Update(entity);
            _context.SaveChanges();
        }
    }
}
