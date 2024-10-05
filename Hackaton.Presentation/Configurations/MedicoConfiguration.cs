using Hackaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackaton.Persistence.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(m => m.MedicoId);
            builder.Property(m => m.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(m => m.CPF).HasColumnType("varchar(15)").IsRequired();
            builder.Property(m => m.CRM).HasColumnType("varchar(15)").IsRequired();
            builder.Property(m => m.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(m => m.Senha).HasColumnType("varchar(20)").IsRequired();
        }
    }
}
