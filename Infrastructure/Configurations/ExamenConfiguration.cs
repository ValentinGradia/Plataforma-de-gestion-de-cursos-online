using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class ExamenConfiguration : IEntityTypeConfiguration<Examen>
{
    public void Configure(EntityTypeBuilder<Examen> builder)
    {
        builder.ToTable("Examenes");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdCurso)
            .HasColumnName("IdCurso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(i => i.IdCurso);

        builder.Property(e => e.TemaExamen)
            .HasColumnName("TemaExamen")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.FechaLimiteDeEntrega)
            .HasColumnName("FechaLimiteDeEntrega")
            .IsRequired();

        builder.Property(e => e.FechaExamenCargado)
            .HasColumnName("FechaExamenCargado")
            .IsRequired();

        builder.Property(e => e.Tipo)
            .HasColumnName("Tipo")
            .HasConversion<int>()
            .IsRequired();
    }
}

