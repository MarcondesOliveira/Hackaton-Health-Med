using Hackaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Persistence
{
    public class HackatonDbContext : DbContext
    {
        public HackatonDbContext(DbContextOptions<HackatonDbContext> options) : base(options)
        {
        }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HackatonDbContext).Assembly);
        }
    }
}
