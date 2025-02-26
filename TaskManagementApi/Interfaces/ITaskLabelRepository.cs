using TaskManagementApi.DTOs;

namespace TaskManagementApi.Interfaces
{
    public interface ITaskLabelRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int firstId, int secondId);
        void Add(TaskLabelCreateDto entity);
        void Update(int taskId, int labelId, TaskLabelUpdateDto entity);
        void Delete(int firstId, int secondId);
    }
}
