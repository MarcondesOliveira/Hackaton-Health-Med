using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Hackaton.Persistence;

namespace Hackaton.Persistence.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(HackatonDbContext dbContext) : base(dbContext)
        {
        }
    }
}
