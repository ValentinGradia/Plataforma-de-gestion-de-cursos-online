using Dapper;
using Microsoft.EntityFrameworkCore;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class ProfesorRepository(ApplicationDbContext dbContext) : Repository<Profesor>(dbContext), IProfesorRepository
{
    public async Task GuardarAsync(Profesor profesor)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"INSERT INTO Usuarios 
                        (Id, Nombre, Apellido, Email, Contraseña, Dni, Rol, Pais, Ciudad, Calle, Altura, FechaRegistro, Especialidad) 
                    VALUES 
                        (@Id, @Nombre, @Apellido, @Email, @Contraseña, @Dni, @Rol, @Pais, @Ciudad, @Calle, @Altura, @FechaRegistro, @Especialidad)";

        await connection.ExecuteAsync(sql, new
        {
            Id            = profesor.Id,
            Nombre        = profesor.Nombre,
            Apellido      = profesor.Apellido,
            Email         = profesor.Email.valorEmail,
            Contraseña    = profesor.Contraseña.ValorContraseña,
            Dni           = profesor.Dni.Valor,
            Rol           = profesor.Rol.ToString(),
            Pais          = profesor.Direccion.Pais,
            Ciudad        = profesor.Direccion.Ciudad,
            Calle         = profesor.Direccion.Calle,
            Altura        = profesor.Direccion.Altura,
            FechaRegistro = profesor.FechaRegistro,
            Especialidad  = profesor.Especialidad
        });
    }

    public async Task ActualizarAsync(Profesor profesor, CancellationToken cancellationToken)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"UPDATE Usuarios 
                    SET Nombre       = @Nombre,
                        Apellido     = @Apellido,
                        Email        = @Email,
                        Pais         = @Pais,
                        Ciudad       = @Ciudad,
                        Calle        = @Calle,
                        Altura       = @Altura,
                        Especialidad = @Especialidad
                    WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id           = profesor.Id,
                Nombre       = profesor.Nombre,
                Apellido     = profesor.Apellido,
                Email        = profesor.Email.valorEmail,
                Pais         = profesor.Direccion.Pais,
                Ciudad       = profesor.Direccion.Ciudad,
                Calle        = profesor.Direccion.Calle,
                Altura       = profesor.Direccion.Altura,
                Especialidad = profesor.Especialidad
            },
            cancellationToken: cancellationToken
        ));
    }

    public async Task<Profesor?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni, 
                           Pais, Ciudad, Calle, Altura, FechaRegistro, Especialidad
                    FROM Usuarios
                    WHERE Id = @Id AND Rol = 'Profesor'";

        var row = await connection.QuerySingleOrDefaultAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)
        );

        if (row is null)
            return null;

        return Profesor.Reconstruir(
            (Guid)row.Id,
            (string)row.Pais,
            (string)row.Ciudad,
            (string)row.Calle,
            (int)row.Altura,
            (string)row.Email,
            (string)row.Contraseña,
            (string)row.Dni,
            (string)row.Nombre,
            (string)row.Apellido,
            (DateTime)row.FechaRegistro,
            (string)row.Especialidad
        );
    }

    public async Task<IEnumerable<Profesor>> ObtenerTodosAsync()
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni,
                           Pais, Ciudad, Calle, Altura, FechaRegistro, Especialidad
                    FROM Usuarios
                    WHERE Rol = 'Profesor'";

        var rows = await connection.QueryAsync(sql);

        return rows.Select(row => Profesor.Reconstruir(
            (Guid)row.Id,
            (string)row.Pais,
            (string)row.Ciudad,
            (string)row.Calle,
            (int)row.Altura,
            (string)row.Email,
            (string)row.Contraseña,
            (string)row.Dni,
            (string)row.Nombre,
            (string)row.Apellido,
            (DateTime)row.FechaRegistro,
            (string)row.Especialidad
        ));
    }
}