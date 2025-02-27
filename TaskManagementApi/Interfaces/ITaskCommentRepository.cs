using TaskManagementApi.DTOs;

namespace TaskManagementApi.Interfaces
{
    public interface ITaskCommentRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
    }
}
