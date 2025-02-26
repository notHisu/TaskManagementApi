using Microsoft.EntityFrameworkCore;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class TaskLabelRepository : ITaskLabelRepository<TaskLabelResponseDto>
    {
        private readonly TaskContext _context;

        public TaskLabelRepository(TaskContext context)
        {
            _context = context;
        }

        private static TaskLabelResponseDto ToDto(TaskLabel taskLabel)
        {
            if (taskLabel == null)
            {
                throw new ArgumentNullException(nameof(taskLabel));
            }
            return new TaskLabelResponseDto
            {
                TaskId = taskLabel.TaskId,
                LabelId = taskLabel.LabelId
            };
        }

        private static IEnumerable<TaskLabelResponseDto> ToDtos(IEnumerable<TaskLabel> taskLabels)
        {
            return taskLabels.Select(ToDto).ToList();
        }

        public void Add(TaskLabelCreateDto entity)
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

            var newTaskLabel = new TaskLabel
            {
                TaskId = entity.TaskId,
                LabelId = entity.LabelId
            };

            _context.TaskLabels.Add(newTaskLabel);
            _context.SaveChanges();
        }

        public void Delete(int taskId, int labelId)
        {
            var taskLabel = _context.TaskLabels.Find(taskId, labelId);
            if (taskLabel == null)
            {
                throw new InvalidOperationException("TaskLabel not found");
            }

            _context.TaskLabels.Remove(taskLabel);
            _context.SaveChanges();
        }

        public IEnumerable<TaskLabelResponseDto> GetAll()
        {
            var taskLabels = _context.TaskLabels.ToList();
            return ToDtos(taskLabels);
        }

        public TaskLabelResponseDto? GetById(int id, int secondId)
        {
            var entity = _context.TaskLabels.Find(id, secondId);
            if (entity == null) {
                throw new InvalidOperationException("TaskLabel not found");
            }
            
            return ToDto(entity);
        }

        public void Update(int taskId, int labelId, TaskLabelUpdateDto entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "TaskLabel cannot be null");
            }

            var taskLabel = _context.TaskLabels.Find(taskId, labelId);
            if (taskLabel == null)
            {
                throw new InvalidOperationException("TaskLabel not found");
            }

            if(entity.LabelId != null)
            {
                var labelExists = _context.Labels.Any(l => l.Id == entity.LabelId);
                if (!labelExists)
                {
                    throw new InvalidOperationException("Label not found");
                }

                taskLabel.LabelId = entity.LabelId.Value;
            }

            _context.SaveChanges();
        }
    }
}
