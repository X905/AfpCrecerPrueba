using ClinicaApp.Data;
using ClinicaApp.Data.Entities;
using ClinicaApp.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly DbConnection connectionString;
        private readonly IPacienteRepository pacienteRepository;

        public PacientesController(IOptions<DbConnection> connectionString, IPacienteRepository pacienteRepository)
        {
            this.connectionString = connectionString.Value;
            this.pacienteRepository = pacienteRepository;
        }

        [HttpGet]
        [Route("Test")]
        public List<Paciente> GetPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();
            using (SqlConnection con = new SqlConnection(this.connectionString.ClinicaAFP))
            {

                string query = "EXECUTE [GetPacientes]";
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

        [HttpGet]
        public List<Paciente> Get()
        {
            List<Paciente> pacientes = new List<Paciente>();

            pacientes = this.pacienteRepository.GetAll("Execute [GetPacientes]",
                x => new Paciente
                {
                    Nombres = (string)x["Nombres"],
                    Apellidos = (string)x["Apellidos"],
                    FechaNacimiento = Convert.ToDateTime(x["FechaNacimiento"]),
                    Id = Convert.ToInt32(x["Id"])
                });

            return pacientes;
        }

        [HttpPost]
        public void Post([FromBody] Paciente paciente)
        {
            SqlParameter Nombres = new SqlParameter("@Nombres", paciente.Nombres);
            SqlParameter Apellidos = new SqlParameter("@Apellidos", paciente.Apellidos);
            SqlParameter FechaNacimiento = new SqlParameter("@FechaNacimiento", paciente.FechaNacimiento);

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

        }
    }


}
