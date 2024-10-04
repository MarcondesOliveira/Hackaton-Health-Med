using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;

namespace Hackaton.Presentation.Repositories
{
    public class ConsultaRepository : BaseRepository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(HackatonDbContext dbContext) : base(dbContext)
        {
        }
    }
}
