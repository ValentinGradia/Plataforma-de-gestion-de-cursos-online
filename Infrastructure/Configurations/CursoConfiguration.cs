using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("cursos");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.IdProfesor)
            .HasColumnName("id_profesor")
            .IsRequired();

        builder.HasOne<Profesor>()
            .WithMany()
            .HasForeignKey(c => c.IdProfesor);

        builder.Property(c => c.Estado)
            .HasColumnName("estado")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.Temario)
            .HasColumnName("temario")
            .HasMaxLength(2000)
            .IsRequired();

        builder.OwnsOne(c => c.Duracion, duracion =>
        {
            duracion.Property(d => d.Inicio)
                .HasColumnName("fecha_inicio")
                .IsRequired();

            duracion.Property(d => d.Fin)
                .HasColumnName("fecha_fin")
                .IsRequired();
        });

        builder.Ignore(c => c.Inscripciones);
        builder.Ignore(c => c.cantidadDeInscriptos);
    }
}

