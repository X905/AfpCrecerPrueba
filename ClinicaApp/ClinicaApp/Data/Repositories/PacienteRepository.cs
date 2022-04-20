using ClinicaApp.Data.Entities;
using ClinicaApp.Data.Interfaces;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaApp.Data.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        private readonly DbConnection connectionString;

        public PacienteRepository(IOptions<DbConnection> connectionString) : base(connectionString)
        {
            this.connectionString = connectionString.Value;
        }

        public bool Update(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                con.Open();

                SqlCommand sqlCmd = new SqlCommand("[dbo].[sp_Edit_Pacientes]", con);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Id", paciente.Id);
                sqlCmd.Parameters.AddWithValue("Nombres", paciente.Nombres);
                sqlCmd.Parameters.AddWithValue("Apellidos", paciente.Apellidos);
                sqlCmd.Parameters.AddWithValue("FechaNacimiento", paciente.FechaNacimiento);
                sqlCmd.ExecuteNonQuery();
            }

            return true;
        }

        bool IPacienteRepository.Create(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("[dbo].[sp_CreatePaciente]", con);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Nombres", paciente.Nombres);
                sqlCmd.Parameters.AddWithValue("Apellidos", paciente.Apellidos);
                sqlCmd.Parameters.AddWithValue("FechaNacimiento", paciente.FechaNacimiento);
                sqlCmd.ExecuteNonQuery();
            }

            return true;
        }
        
        bool IPacienteRepository.Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("[dbo].[sp_DeletePaciente]", con);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Id", id);
                sqlCmd.ExecuteNonQuery();
            }

            return true;
        }
    }
}
