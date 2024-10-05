using Hackaton.Domain.Entities;

namespace Hackaton.Domain.Interfaces
{
    public interface IMedicoRepository : IAsyncRepository<Medico>
    {
        Task<Medico?> GetByEmailAndPasswordAsync(string email, string senha);
    }
}
