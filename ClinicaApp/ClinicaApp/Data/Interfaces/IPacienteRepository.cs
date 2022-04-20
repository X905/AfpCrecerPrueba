using ClinicaApp.Data.Entities;

namespace ClinicaApp.Data.Interfaces
{
    public interface IPacienteRepository: IGenericRepository<Paciente>
    {
        bool Create(Paciente paciente);

        bool Update(Paciente paciente);
        bool Delete(int id);
    }
}
