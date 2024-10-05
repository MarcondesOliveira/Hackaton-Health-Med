using Hackaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackaton.Persistence.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(m => m.PacienteId);
            builder.Property(m => m.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(m => m.CPF).HasColumnType("varchar(15)").IsRequired();
            builder.Property(m => m.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(m => m.Senha).HasColumnType("varchar(20)").IsRequired();
        }
    }
}
