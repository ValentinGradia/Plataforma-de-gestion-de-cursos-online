using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class EncuestaConfiguration : IEntityTypeConfiguration<Encuesta>
{
    public void Configure(EntityTypeBuilder<Encuesta> builder)
    {
        builder.ToTable("encuestas");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdCurso)
            .HasColumnName("id_curso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(e => e.IdCurso);

        builder.Property(e => e.IdEstudiante)
            .HasColumnName("id_estudiante");
        
        builder.HasOne<Estudiante>()
            .WithMany()
            .HasForeignKey(e => e.IdEstudiante);

        builder.Property(e => e.CalificacionCurso)
            .HasColumnName("calificacion_curso")
            .HasConversion(
                c => c.Valor,
                v => new Calificacion(v))
            .IsRequired();

        builder.Property(e => e.CalificacionMaterial)
            .HasColumnName("calificacion_material")
            .HasConversion(
                c => c.Valor,
                v => new Calificacion(v))
            .IsRequired();

        builder.Property(e => e.CalificacionDocente)
            .HasColumnName("calificacion_docente")
            .HasConversion(
                c => c.Valor,
                v => new Calificacion(v))
            .IsRequired();

        builder.Property(e => e.Comentarios)
            .HasColumnName("comentarios")
            .HasMaxLength(2000);

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();
    }
}

