using ClinicaApp.Data.Entities;
using ClinicaApp.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace ClinicaApp.Data.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        private readonly DbConnection connectionString;

        public PacienteRepository(IOptions<DbConnection> connectionString) : base(connectionString)
        {
            this.connectionString = connectionString.Value;
        }

    }
}
