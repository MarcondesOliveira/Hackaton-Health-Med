using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Persistence.Repositories
{
    public class MedicoRepository : BaseRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(HackatonDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Medico?> GetByEmailAndPasswordAsync(string email, string senha)
        {
            // Buscar o médico pelo email
            var medico = await _dbContext.Medicos.FirstOrDefaultAsync(m => m.Email == email);

            // Verificar se a senha está correta usando um método de hash, como BCrypt
            if (medico != null && BCrypt.Net.BCrypt.Verify(senha, medico.Senha))
            {
                return medico;
            }

            // Se não encontrar ou se a senha estiver errada, retorna null
            return null;
        }
    }
}
