using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces
{
    public interface ITaskRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Delete(int id);
        void Add(TaskCreateDto entity);
        void Update(int id, TaskUpdateDto entity);
    }
}
