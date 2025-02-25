﻿namespace TaskManagementApi.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int firstId, int? secondId = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(int firstId, int? secondId = null);
    }
}
