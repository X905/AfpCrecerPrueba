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
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("GetPaciente")]
        public async Task<Paciente> Get(int id)
        {
            Paciente paciente = new Paciente();

            paciente = await this.pacienteRepository.GetByIdAsync("Execute [dbo].[sp_GetPacienteByID]", id,
                x => new Paciente
                {
                    Nombres = (string)x["Nombres"],
                    Apellidos = (string)x["Apellidos"],
                    FechaNacimiento = Convert.ToDateTime(x["FechaNacimiento"]),
                    Id = Convert.ToInt32(x["Id"])
                });

            return paciente;
        }

        [HttpPost]
        public void Post([FromBody] Paciente paciente)
        {
            this.pacienteRepository.CreateAsync(paciente);
        }

        [HttpPost]
        [Route("Edit")]
        public void Edit([FromBody] Paciente paciente)
        {
            this.pacienteRepository.UpdateAsync(paciente);
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            this.pacienteRepository.DeleteAsync(id);
        }
    }


}
