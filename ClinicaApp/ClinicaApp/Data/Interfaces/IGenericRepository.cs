using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ClinicaApp.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll<T>(string query, Func<DbDataReader, T> map);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        void Create(T entity);

        Task<T> GetByIdAsync(int id);
    }
}
