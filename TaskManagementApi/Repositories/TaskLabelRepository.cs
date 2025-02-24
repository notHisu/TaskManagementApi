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
            if(entity == null)
            {
                throw new ArgumentNullException("TaskLabel cannot be null");
            }

            var task = _context.TaskItems.FirstOrDefault(t => t.Id == entity.TaskId);
            var label = _context.Labels.FirstOrDefault(label => label.Id == entity.LabelId);

            if(task == null)
            {
                throw new NullReferenceException("Task not found");
            }

            if(label == null)
            {
                throw new NullReferenceException("Label not found");
            }

            var taskLabel = _context.TaskLabels.Find(entity.TaskId, entity.LabelId);

            if(taskLabel != null)
            {
                throw new Exception("Already exists");
            }
                        
            _context.TaskLabels.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int taskId, int? labelId = null)
        {
            var taskLabel = _context.TaskLabels.Find(taskId, labelId);
            if(taskLabel == null) {
                throw new NullReferenceException("TaskLabel not found");
            }

            _context.TaskLabels.Remove(taskLabel);
            _context.SaveChanges();
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
