using TaskManagementApi.DTOs;

namespace TaskManagementApi.Interfaces
{
    public interface ITaskCommentRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Delete(int id);
        void Add(TaskCommentCreateDto entity);
        void Update(int id, TaskCommentUpdateDto entity);
    }
}
