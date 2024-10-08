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
            var medico = await _dbContext.Medicos.FirstOrDefaultAsync(m => m.Email == email);

            if (medico != null && BCrypt.Net.BCrypt.Verify(senha, medico.Senha))
            {
                return medico;
            }

            return null;
        }

        public async Task<Medico> GetByIdWithConsultasAsync(Guid medicoId)
        {
            return await _dbContext.Medicos
                                 .Include(m => m.Consultas) // Inclui as consultas relacionadas
                                 .FirstOrDefaultAsync(m => m.MedicoId == medicoId)
                                    ?? throw new Exception("Esse médico nao tem consultas");
        }

        public async Task<List<Medico>> ListAllWithConsultasAsync()
        {
            return await _dbContext.Medicos
                                 .Include(m => m.Consultas) // Inclui as consultas relacionadas a cada médico
                                 .ToListAsync();
        }

    }
}
