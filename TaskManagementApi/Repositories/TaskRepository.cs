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

        public bool CheckUserExists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }

        public bool CheckCategoryExists(int id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }

        public void Add(TaskItem entity)
        {
            if (entity == null) {
                throw new ArgumentNullException("Task cannot be null");
            }

            bool userExists = CheckUserExists(entity.UserId);
            bool categoryExists = CheckCategoryExists(entity.CategoryId);

            if (!userExists)
            {
                throw new NullReferenceException("User not found");
            }

            if (!categoryExists) {
                throw new NullReferenceException("Category not found");
            }

            _context.TaskItems.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id, int? secondId = null)
        {
            var task = _context.TaskItems.FirstOrDefault(x => x.Id == id);

            if(task != null)
            {
                _context.TaskItems.Remove(task);
                _context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException("Task not found");
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
                throw new NullReferenceException("Task not found");
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
                throw new NullReferenceException("Task not found");
            }

            _context.TaskItems.Update(entity);
            _context.SaveChanges();
        }
    }
}
