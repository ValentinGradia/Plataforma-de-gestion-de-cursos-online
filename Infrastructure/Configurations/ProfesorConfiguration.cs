using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class ProfesorConfiguration : IEntityTypeConfiguration<Profesor>
{
    public void Configure(EntityTypeBuilder<Profesor> builder)
    {
        builder.Property(p => p.Especialidad)
            .HasColumnName("especialidad")
            .HasMaxLength(100);
    }
}

