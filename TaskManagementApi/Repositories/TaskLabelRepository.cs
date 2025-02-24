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
            throw new NotImplementedException();
        }

        public IEnumerable<TaskLabel> GetAll()
        {
            return _context.TaskLabels.ToList();
        }

        public TaskLabel? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskLabel entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("TaskLabel cannot be null");
            }

            _context.TaskLabels.Update(entity);
            _context.SaveChanges();
        }
    }
}
