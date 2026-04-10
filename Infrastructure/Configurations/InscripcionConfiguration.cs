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
            .HasColumnName("id_estudiante")
            .IsRequired();

        builder.HasOne<Estudiante>()
            .WithMany()
            .HasForeignKey(i => i.IdEstudiante);

        builder.Property(i => i.IdCurso)
            .HasColumnName("id_curso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(i => i.IdCurso);

        builder.Property(i => i.FechaInscripcion)
            .HasColumnName("fecha_inscripcion")
            .IsRequired();

        builder.Property(i => i.Activa)
            .HasColumnName("activa")
            .IsRequired();

        builder.Property(i => i.porcentajeAsistencia)
            .HasColumnName("porcentaje_asistencia")
            .IsRequired();

        builder.Ignore("_historialEntregas");
        builder.Ignore("_asistenciasDelEstudiante");
    }
}

