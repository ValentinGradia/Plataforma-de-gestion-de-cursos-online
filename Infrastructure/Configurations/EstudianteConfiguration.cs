using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
{
    public void Configure(EntityTypeBuilder<Estudiante> builder)
    {
        builder.ToTable("estudiantes");

        builder.HasKey(e => e.Id);

        // Mantiene la PK sincronizada con Usuario cuando hay herencia.
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        // builder.Property(e => e.CursosInscritosActualmente)
        //     .HasColumnName("cursos_inscritos_actualmente")
        //     .HasMaxLength(4000)
        //     .HasConversion(cursos => cursos)
        //     .HasConversion(
        //         cursos => string.Join(",", cursos
        //             .Where(id => id.HasValue)
        //             .Select(id => id!.Value.ToString())),
        //         valor => string.IsNullOrWhiteSpace(valor)
        //             ? new List<Guid?>()
        //             : valor.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                 .Select(Guid.Parse)
        //                 .Select(id => (Guid?)id)
        //                 .ToList());
        //
        // builder.Property(e => e.HistorialDeCursos)
        //     .HasColumnName("historial_de_cursos")
        //     .HasMaxLength(4000)
        //     .HasConversion(
        //         historial => string.Join(",", historial
        //             .Where(id => id.HasValue)
        //             .Select(id => id!.Value.ToString())),
        //         valor => string.IsNullOrWhiteSpace(valor)
        //             ? new List<Guid?>()
        //             : valor.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                 .Select(Guid.Parse)
        //                 .Select(id => (Guid?)id)
        //                 .ToList());
    }
}
