namespace TaskManagementApi.Interfaces
{
    public interface ITaskLabelRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int firstId, int secondId);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(int taskId, int labelId);
        Task DeleteAsync(int firstId, int secondId);
    }
}
