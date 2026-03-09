using Dapper;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class EstudianteRepository(IDbConnectionFactory _connectionFactory) : IEstudianteRepository
{
    public async Task GuardarAsync(Estudiante estudiante)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"INSERT INTO Usuarios 
                        (Id, Nombre, Apellido, Email, Contraseña, Dni, Rol, Pais, Ciudad, Calle, Altura, FechaRegistro) 
                    VALUES 
                        (@Id, @Nombre, @Apellido, @Email, @Contraseña, @Dni, @Rol, @Pais, @Ciudad, @Calle, @Altura, @FechaRegistro)";

        await connection.ExecuteAsync(sql, new
        {
            Id            = estudiante.Id,
            Nombre        = estudiante.Nombre,
            Apellido      = estudiante.Apellido,
            Email         = estudiante.Email.valorEmail,
            Contraseña    = estudiante.Contraseña.ValorContraseña,
            Dni           = estudiante.Dni.Valor,
            Rol           = estudiante.Rol.ToString(),
            Pais          = estudiante.Direccion.Pais,
            Ciudad        = estudiante.Direccion.Ciudad,
            Calle         = estudiante.Direccion.Calle,
            Altura        = estudiante.Direccion.Altura,
            FechaRegistro = estudiante.FechaRegistro
        });
    }

    public async Task ActualizarAsync(Estudiante estudiante, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"UPDATE Usuarios 
                    SET Nombre    = @Nombre,
                        Apellido  = @Apellido,
                        Email     = @Email,
                        Pais      = @Pais,
                        Ciudad    = @Ciudad,
                        Calle     = @Calle,
                        Altura    = @Altura
                    WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id       = estudiante.Id,
                Nombre   = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Email    = estudiante.Email.valorEmail,
                Pais     = estudiante.Direccion.Pais,
                Ciudad   = estudiante.Direccion.Ciudad,
                Calle    = estudiante.Direccion.Calle,
                Altura   = estudiante.Direccion.Altura
            },
            cancellationToken: cancellationToken
        ));
    }

    public async Task<Estudiante?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni, 
                           Pais, Ciudad, Calle, Altura, FechaRegistro
                    FROM Usuarios
                    WHERE Id = @Id AND Rol = 'Estudiante'";

        var row = await connection.QuerySingleOrDefaultAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)
        );

        if (row is null)
            return null;

        return Estudiante.ReconstruirEstudiante(row);
    }

    public async Task<IEnumerable<Estudiante>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni,
                           Pais, Ciudad, Calle, Altura, FechaRegistro
                    FROM Usuarios
                    WHERE Rol = 'Estudiante'";

        var rows = await connection.QueryAsync(sql);

        return rows.Select(row => Estudiante.ReconstruirEstudiante(row)).Cast<Estudiante>();
    }

    public async Task<List<Estudiante>> ObtenerPorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni,
                           Pais, Ciudad, Calle, Altura, FechaRegistro
                    FROM Usuarios
                    WHERE Id IN @Ids AND Rol = 'Estudiante'";

        var rows = await connection.QueryAsync(
            new CommandDefinition(sql, new { Ids = ids }, cancellationToken: cancellationToken)
        );

        return rows.Select(row => Estudiante.ReconstruirEstudiante(row)).Cast<Estudiante>().ToList();
    }
    
}