using Microsoft.EntityFrameworkCore;
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
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "TaskLabel cannot be null");
            }

            var taskExists = _context.TaskItems.Any(t => t.Id == entity.TaskId);
            var labelExists = _context.Labels.Any(l => l.Id == entity.LabelId);

            if (!taskExists)
            {
                throw new InvalidOperationException("Task not found");
            }

            if (!labelExists)
            {
                throw new InvalidOperationException("Label not found");
            }

            var existingTaskLabel = _context.TaskLabels.Find(entity.TaskId, entity.LabelId);
            if (existingTaskLabel != null)
            {
                throw new InvalidOperationException("This task label pair already exists");
            }

            _context.TaskLabels.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int taskId, int? labelId = null)
        {
            var taskLabel = _context.TaskLabels.Find(taskId, labelId);
            if (taskLabel == null)
            {
                throw new InvalidOperationException("TaskLabel not found");
            }

            _context.TaskLabels.Remove(taskLabel);
            _context.SaveChanges();
        }

        public IEnumerable<TaskLabel> GetAll()
        {
             return _context.TaskLabels.ToList();
        }

        public TaskLabel? GetById(int id, int? secondId = null)
        {
            var entity = _context.TaskLabels.Find(id, secondId);
            if (entity == null) {
                throw new InvalidOperationException("TaskLabel not found");
            }

            return entity;
        }

        public void Update(TaskLabel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "TaskLabel cannot be null");
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
