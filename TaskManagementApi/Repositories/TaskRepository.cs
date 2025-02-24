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
            _context.TaskItems.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.TaskItems.Remove(GetById(id)!);
            _context.SaveChanges();
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        public TaskItem? GetById(int id)
        {
            return _context.TaskItems.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TaskItem entity)
        {
            _context.TaskItems.Update(entity);
            _context.SaveChanges();
        }
    }
}
