using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Hackaton.Persistence;

namespace Hackaton.Persistence.Repositories
{
    public class ConsultaRepository : BaseRepository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(HackatonDbContext dbContext) : base(dbContext)
        {
        }
    }
}
