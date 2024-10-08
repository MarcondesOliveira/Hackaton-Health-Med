using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Hackaton.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Persistence.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(HackatonDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Paciente?> GetByEmailAndPasswordAsync(string email, string senha)
        {
            var paciente = await _dbContext.Pacientes.FirstOrDefaultAsync(m => m.Email == email);

            if (paciente != null && BCrypt.Net.BCrypt.Verify(senha, paciente.Senha))
            {
                return paciente;
            }

            return null;
        }
    }
}
