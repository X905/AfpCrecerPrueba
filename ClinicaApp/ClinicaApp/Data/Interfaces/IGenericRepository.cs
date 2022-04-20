using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ClinicaApp.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll<T>(string query, Func<DbDataReader, T> map);
        Task UpdateAsync(Dictionary<string, object> sqlParams, string query);
        Task DeleteAsync(Dictionary<string, object> sqlParams, string query);
        Task CreateAsync(Dictionary<string, object> sqlParams, string query);
        Task<T> GetByIdAsync(string query, int Id, Func<DbDataReader, T> map);
    }
}
