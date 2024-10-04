using Hackaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackaton.Presentation.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(m => m.ConsultaId);
            builder.Property(m => m.ConsultaId).HasColumnType("uniqueidentifier");
            builder.Property(m => m.MedicoId).HasColumnType("uniqueidentifier");
            builder.Property(m => m.PacienteId).HasColumnType("uniqueidentifier");
            builder.Property(m => m.Data).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.Status).IsRequired().HasDefaultValue(Status.Disponivel);
        }
    }
}