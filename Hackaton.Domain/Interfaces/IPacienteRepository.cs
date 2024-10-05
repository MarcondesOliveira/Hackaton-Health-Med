using Hackaton.Domain.Entities;

namespace Hackaton.Domain.Interfaces
{
    public interface IPacienteRepository : IAsyncRepository<Paciente>
    {
        Task<Paciente?> GetByEmailAndPasswordAsync(string email, string senha);
    }
}
