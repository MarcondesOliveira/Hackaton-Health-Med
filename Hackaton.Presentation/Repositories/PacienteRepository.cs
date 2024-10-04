using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;

namespace Hackaton.Presentation.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(HackatonDbContext dbContext) : base(dbContext)
        {
        }
    }
}
