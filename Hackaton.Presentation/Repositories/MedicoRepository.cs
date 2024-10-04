using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;

namespace Hackaton.Presentation.Repositories
{
    public class MedicoRepository : BaseRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(HackatonDbContext dbContext) : base(dbContext)
        {
        }
    }
}
