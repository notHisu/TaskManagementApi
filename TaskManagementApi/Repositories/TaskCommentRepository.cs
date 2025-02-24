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
            if(entity == null)
            {
                throw new ArgumentNullException("TaskComment cannot be null");
            }

            _context.TaskComments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id, int? secondId = null)
        {
            var taskComment = _context.TaskComments.FirstOrDefault(c => c.Id == id);

            if(taskComment != null) {
                _context.TaskComments.Remove(taskComment);
                _context.SaveChanges();
            }
        }

        public IEnumerable<TaskComment> GetAll()
        {
            return _context.TaskComments.ToList();
        }

        public TaskComment? GetById(int id)
        {
            var taskComment = _context.TaskComments.FirstOrDefault(x => x.Id == id);
            
            if(taskComment == null) {
                throw new NullReferenceException("TaskComment not found");
            }
            
            return taskComment;
        }

        public void Update(TaskComment entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("TaskComment cannot be null");
            }

            _context.TaskComments.Update(entity);
            _context.SaveChanges();
        }
    }
}
