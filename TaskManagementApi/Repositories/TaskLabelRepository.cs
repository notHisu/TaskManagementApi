using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class TaskLabelRepository : IGenericRepository<TaskLabel>
    {
        private readonly TaskContext _context;

        public TaskLabelRepository(TaskContext context)
        {
            _context = context;
        }

        public void Add(TaskLabel entity)
        {
            _context.TaskLabels.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.TaskLabels.Remove(GetById(id)!);
        }

        public IEnumerable<TaskLabel> GetAll()
        {
            return _context.TaskLabels.ToList();
        }

        public TaskLabel? GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TaskLabel entity)
        {
            _context.TaskLabels.Update(entity);
            _context.SaveChanges();
        }
    }
}
