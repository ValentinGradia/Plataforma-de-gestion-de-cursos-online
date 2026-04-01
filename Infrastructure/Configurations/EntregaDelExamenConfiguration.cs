using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class EntregaDelExamenConfiguration : IEntityTypeConfiguration<EntregaDelExamen>
{
    public void Configure(EntityTypeBuilder<EntregaDelExamen> builder)
    {
        builder.ToTable("entregas_examen");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdExamen)
            .HasColumnName("id_examen")
            .IsRequired();
        
        builder.HasOne<Examen>()
            .WithMany()
            .HasForeignKey(e => e.IdExamen);

        builder.Property(e => e.IdInscripcionEstudiante)
            .HasColumnName("id_inscripcion_estudiante")
            .IsRequired();
        
        builder.HasOne<Inscripcion>()
            .WithMany()
            .HasForeignKey(e => e.IdInscripcionEstudiante);

        builder.Property(e => e.Tipo)
            .HasColumnName("tipo")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.Respuesta)
            .HasColumnName("respuesta")
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(e => e.FechaEntregado)
            .HasColumnName("fecha_entregado")
            .IsRequired();

        builder.Property(e => e.ComentarioDocente)
            .HasColumnName("comentario_docente")
            .HasMaxLength(2000);

        builder.Property(e => e.Nota)
            .HasColumnName("nota")
            .HasConversion(
                nota => nota == null ? (decimal?)null : nota.Valor,
                valor => valor.HasValue ? new Nota(valor.Value) : null);
    }
}

