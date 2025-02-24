using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class TaskCommentRepository : IGenericRepository<TaskComment>
    {
        private readonly TaskContext _context;

        public TaskCommentRepository(TaskContext context)
        {
            _context = context;
        }
        public void Add(TaskComment entity)
        {
            _context.TaskComments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.TaskComments.Remove(GetById(id)!);
            _context.SaveChanges();
        }

        public IEnumerable<TaskComment> GetAll()
        {
            return _context.TaskComments.ToList();
        }

        public TaskComment? GetById(int id)
        {
            return _context.TaskComments.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TaskComment entity)
        {
            _context.TaskComments.Update(entity);
            _context.SaveChanges();
        }
    }
}
