using ClinicaApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly DbConnection connectionString;

        public PacientesController(IOptions<DbConnection> connectionString)
        {
            this.connectionString = connectionString.Value;
        }

        [HttpGet]
        [Route("pacientes")]
        public List<Paciente> GetPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();
            using (SqlConnection con = new SqlConnection(this.connectionString.ClinicaAFP))
            {
                string query = "SELECT Id ,Nombres, Apellidos, FechaNacimiento FROM Paciente";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            pacientes.Add(new Paciente
                            {
                                Nombres = sdr["Nombres"].ToString(),
                                Apellidos = sdr["Apellidos"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(sdr["FechaNacimiento"].ToString()),
                                Id = Convert.ToInt32(sdr["Id"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return pacientes;
        }

    }

   
}
