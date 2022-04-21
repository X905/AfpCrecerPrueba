using ClinicaApp.Data;
using ClinicaApp.Data.Entities;
using ClinicaApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        [Route("Create")]
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
