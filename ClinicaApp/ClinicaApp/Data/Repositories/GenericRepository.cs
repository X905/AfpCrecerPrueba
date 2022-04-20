using ClinicaApp.Data.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaApp.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbConnection connectionString;

        public GenericRepository(IOptions<DbConnection> connectionString)
        {
            this.connectionString = connectionString.Value;
        }

        public void Create(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }


        public List<T> GetAll<T>(string query, Func<DbDataReader, T> map)
        {
            using (SqlConnection command = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = command;
                    command.Open();
                    var entities = new List<T>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            entities.Add(map(sdr));
                        }

                    }
                    command.Close();
                    return entities;
                }
            }

        }

        public async Task<T> GetByIdAsync(string query, int Id, Func<DbDataReader, T> map)
        {
            using (SqlConnection command = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                using (SqlCommand cmd = new SqlCommand(query + " " + Id))
                {
                    cmd.Connection = command;
                    command.Open();
                    var entities = new List<T>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            entities.Add(map(sdr));
                        }

                    }
                    command.Close();
                    return entities.FirstOrDefault();
                }
            }
        }

        public Task UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
