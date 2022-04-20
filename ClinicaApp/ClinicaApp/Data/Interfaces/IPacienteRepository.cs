using ClinicaApp.Data.Entities;
using System.Threading.Tasks;

namespace ClinicaApp.Data.Interfaces
{
    public interface IPacienteRepository: IGenericRepository<Paciente>
    {
        Task<bool> CreateAsync(Paciente paciente);

        Task<bool> UpdateAsync(Paciente paciente);
        Task<bool> DeleteAsync(int id);
    }
}
