using ClinicaApp.Data.Entities;
using ClinicaApp.Data.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ClinicaApp.Data.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        private readonly DbConnection connectionString;

        public PacienteRepository(IOptions<DbConnection> connectionString) : base(connectionString)
        {
            this.connectionString = connectionString.Value;
        }

        async Task<bool> IPacienteRepository.UpdateAsync(Paciente paciente)
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>()
            {
                { "Id", paciente.Id},
                { "Nombres", paciente.Nombres},
                { "Apellidos", paciente.Apellidos},
                { "FechaNacimiento", paciente.FechaNacimiento}
            };

            await this.UpdateAsync(sqlParams, "[dbo].[sp_Edit_Pacientes]");
            return true;
        }

        async Task<bool> IPacienteRepository.CreateAsync(Paciente paciente)
        {

            Dictionary<string, object> sqlParams = new Dictionary<string, object>()
            {
                { "Nombres", paciente.Nombres},
                { "Apellidos", paciente.Apellidos},
                { "FechaNacimiento", paciente.FechaNacimiento}
            };

            await this.CreateAsync(sqlParams, "[dbo].[sp_CreatePaciente]");
            return true;
        }

        async Task<bool> IPacienteRepository.DeleteAsync(int id)
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>()
            {
                { "Id", id}
            };
            await this.DeleteAsync(sqlParams, "[dbo].[sp_DeletePaciente]");
            return true;
        }
            
    }
}
