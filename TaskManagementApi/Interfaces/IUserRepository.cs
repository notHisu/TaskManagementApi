using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Delete(int id);
        T Add(UserCreateDto entity);
        T? Update(int id, UserUpdateDto entity);
        T? ValidateCredentials(string username, string password);
    }
}
