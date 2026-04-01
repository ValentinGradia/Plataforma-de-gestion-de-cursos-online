using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class ExamenConfiguration : IEntityTypeConfiguration<Examen>
{
    public void Configure(EntityTypeBuilder<Examen> builder)
    {
        builder.ToTable("examenes");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdCurso)
            .HasColumnName("id_curso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(i => i.IdCurso);

        builder.Property(e => e.TemaExamen)
            .HasColumnName("tema_examen")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.FechaLimiteDeEntrega)
            .HasColumnName("fecha_limite_entrega")
            .IsRequired();

        builder.Property(e => e.FechaExamenCargado)
            .HasColumnName("fecha_examen_cargado")
            .IsRequired();

        builder.Property(e => e.Tipo)
            .HasColumnName("tipo")
            .HasConversion<int>()
            .IsRequired();
    }
}

