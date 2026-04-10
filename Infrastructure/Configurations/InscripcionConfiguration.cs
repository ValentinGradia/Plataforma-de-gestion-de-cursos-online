using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class InscripcionConfiguration : IEntityTypeConfiguration<Inscripcion>
{
    public void Configure(EntityTypeBuilder<Inscripcion> builder)
    {
        builder.ToTable("Inscripciones");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.IdEstudiante)
            .HasColumnName("IdEstudiante")
            .IsRequired();

        builder.HasOne<Estudiante>()
            .WithMany()
            .HasForeignKey(i => i.IdEstudiante);

        builder.Property(i => i.IdCurso)
            .HasColumnName("IdCurso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(i => i.IdCurso);

        builder.Property(i => i.FechaInscripcion)
            .HasColumnName("FechaInscripcion")
            .IsRequired();

        builder.Property(i => i.Activa)
            .HasColumnName("Activa")
            .IsRequired();

        builder.Property(i => i.porcentajeAsistencia)
            .HasColumnName("PorcentajeAsistencia")
            .IsRequired();

        builder.Ignore("_historialEntregas");
        builder.Ignore("_asistenciasDelEstudiante");
    }
}

