using ClinicaApp.Data.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
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

     

        /// <summary>
        /// Ejecutar procedimientos almacenados con parametros, el diccionario recibe clave "Nombre del parametro" valor "Objeto"
        /// </summary>
        /// <param name="keyValuesParams"></param>
        /// <param name="query"></param>
        public async Task ExecuteWithParameter(Dictionary<string, object> keyValuesParams, string query)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand(query, con);
                foreach(var keyValueParam in keyValuesParams)
                {
                    sqlCmd.Parameters.AddWithValue(keyValueParam.Key, keyValueParam.Value);
                }
                sqlCmd.CommandType = CommandType.StoredProcedure;
                await sqlCmd.ExecuteNonQueryAsync();
                con.Close();
            }
        }
        /// <summary>
        /// Ejecuta el procedimiento almacenado para creacion de un registro
        /// </summary>
        /// <param name="sqlparam"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public async Task CreateAsync(Dictionary<string, object> sqlparam, string storedProcedure)
        {
            await ExecuteWithParameter(sqlparam, storedProcedure);
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado para la edicion de un registro
        /// </summary>
        /// <param name="sqlparam"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Dictionary<string, object> sqlparam, string storedProcedure)
        {
            await ExecuteWithParameter(sqlparam, storedProcedure);
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado para la eliminacion de un registro
        /// </summary>
        /// <param name="sqlparam"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> sqlparam, string storedProcedure)
        {
            await ExecuteWithParameter(sqlparam, storedProcedure);
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

      
    }
}
