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
        builder.ToTable("Encuestas");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdCurso)
            .HasColumnName("IdCurso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(e => e.IdCurso);

        builder.Property(e => e.IdEstudiante)
            .HasColumnName("IdEstudiante");
        
        builder.HasOne<Estudiante>()
            .WithMany()
            .HasForeignKey(e => e.IdEstudiante);

        builder.Property(e => e.CalificacionCurso)
            .HasColumnName("CalificacionCurso")
            .HasConversion(
                c => c.Valor,
                v => new Calificacion(v))
            .IsRequired();

        builder.Property(e => e.CalificacionMaterial)
            .HasColumnName("CalificacionMaterial")
            .HasConversion(
                c => c.Valor,
                v => new Calificacion(v))
            .IsRequired();

        builder.Property(e => e.CalificacionDocente)
            .HasColumnName("CalificacionDocente")
            .HasConversion(
                c => c.Valor,
                v => new Calificacion(v))
            .IsRequired();

        builder.Property(e => e.Comentarios)
            .HasColumnName("Comentarios")
            .HasMaxLength(2000);

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("FechaCreacion")
            .IsRequired();
    }
}

