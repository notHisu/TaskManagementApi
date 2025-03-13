using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Security.Claims;

namespace TaskManagementApi.Repositories
{
    public class TaskLabelRepository : ITaskLabelRepository<TaskLabel>
    {
        private readonly TaskContext _context;

        public TaskLabelRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<TaskLabel> AddAsync(TaskLabel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "TaskLabel cannot be null");
            }

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == entity.TaskId);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            var labelExists = await _context.Labels.AnyAsync(l => l.Id == entity.LabelId);
            if (!labelExists)
            {
                throw new KeyNotFoundException("Label not found");
            }

            var existingTaskLabel = await _context.TaskLabels.FindAsync(entity.TaskId, entity.LabelId);
            if (existingTaskLabel != null)
            {
                throw new InvalidOperationException("This task-label pair already exists");
            }

            await _context.TaskLabels.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int taskId, int labelId)
        {
            var taskLabel = await _context.TaskLabels.FindAsync(taskId, labelId);
            if (taskLabel == null)
            {
                throw new KeyNotFoundException("TaskLabel not found");
            }

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _context.TaskLabels.Remove(taskLabel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskLabel>> GetAllAsync()
        {
            return await _context.TaskLabels.AsNoTracking().ToListAsync();
        }

        public async Task<TaskLabel?> GetByIdAsync(int taskId, int labelId)
        {
            return await _context.TaskLabels.AsNoTracking()
                .FirstOrDefaultAsync(tl => tl.TaskId == taskId && tl.LabelId == labelId);
        }

        public async Task<TaskLabel> UpdateAsync(int taskId, int labelId)
        {
            var taskLabel = await _context.TaskLabels.FindAsync(taskId, labelId);

            if (taskLabel != null)
            {
                throw new InvalidOperationException("This pair is aldready existed");
            }

            var newTaskLabel = new TaskLabel
            {
                TaskId = taskId,
                LabelId = labelId
            };

            await _context.TaskLabels.AddAsync(newTaskLabel);
            await _context.SaveChangesAsync();

            return newTaskLabel;

        }

        public async Task<IEnumerable<TaskLabel>> GetTaskLabelsAsync(int taskId)
        {
            return await _context.TaskLabels.AsNoTracking()
                .Where(tl => tl.TaskId == taskId)
                .ToListAsync();
        }
    }
}
