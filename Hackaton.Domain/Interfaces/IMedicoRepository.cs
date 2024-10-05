using Hackaton.Domain.Entities;

namespace Hackaton.Domain.Interfaces
{
    public interface IMedicoRepository : IAsyncRepository<Medico>
    {
        Task<Medico?> GetByEmailAndPasswordAsync(string email, string senha);
        Task<Medico> GetByIdWithConsultasAsync(Guid medicoId);
        Task<List<Medico>> ListAllWithConsultasAsync();
    }
}
