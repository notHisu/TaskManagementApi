using TaskManagementApi.DTOs;

namespace TaskManagementApi.Interfaces
{
    public interface ICategoryRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Delete(int id);
        void Add(CategoryCreateDto entity);
        void Update(int id, CategoryUpdateDto entity);
    }
}
