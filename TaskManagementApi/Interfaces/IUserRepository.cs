using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(string id);
        void Delete(string id);
        T Add(UserCreateDto entity);
        T? Update(string id, UserUpdateDto entity);
    }
}
                                