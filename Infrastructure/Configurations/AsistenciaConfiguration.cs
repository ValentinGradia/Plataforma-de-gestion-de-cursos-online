using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
{
    public void Configure(EntityTypeBuilder<Asistencia> builder)
    {
        builder.ToTable("Asistencias");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.IdInscripcionEstudiante)
            .HasColumnName("IdInscripcionEstudiante")
            .IsRequired();
        
        builder.HasOne<Inscripcion>()
            .WithMany()
            .HasForeignKey(a => a.IdInscripcionEstudiante);

        builder.Property(a => a.IdClase)
            .HasColumnName("IdClase")
            .IsRequired();
        
        builder.HasOne<Clase>()
            .WithMany()
            .HasForeignKey(a => a.IdClase);

        builder.Property(a => a.Presente)
            .HasColumnName("Presente")
            .IsRequired();
    }
}

