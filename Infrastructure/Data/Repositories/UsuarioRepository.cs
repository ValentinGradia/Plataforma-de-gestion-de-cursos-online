using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class UsuarioRepository(ApplicationDbContext dbContext) : Repository<Usuario>(dbContext), IUsuarioRepository
{
    public async Task GuardarAsync(Usuario objeto)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"INSERT INTO Usuarios 
                        (Id, Nombre, Apellido, Email, Contraseña, Dni, Rol, Pais, Ciudad, Calle, Altura, FechaRegistro) 
                    VALUES 
                        (@Id, @Nombre, @Apellido, @Email, @Contrasena, @Dni, @Rol, @Pais, @Ciudad, @Calle, @Altura, @FechaRegistro)";

        await connection.ExecuteAsync(sql, new
        {
            Id           = objeto.Id,
            Nombre       = objeto.Nombre,
            Apellido     = objeto.Apellido,
            Email        = objeto.Email.valorEmail,
            Contraseña   = objeto.Contraseña.ValorContraseña,
            Dni          = objeto.Dni.Valor,
            Rol          = objeto.Rol.ToString(),
            Pais         = objeto.Direccion.Pais,
            Ciudad       = objeto.Direccion.Ciudad,
            Calle        = objeto.Direccion.Calle,
            Altura       = objeto.Direccion.Altura,
            FechaRegistro = objeto.FechaRegistro
        });
    }

    public async Task ActualizarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email WHERE Id = @Id";
        await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = usuario.Id, Nombre = usuario.Nombre, Apellido = usuario.Apellido, Email = usuario.Email.valorEmail}, cancellationToken: cancellationToken));
    }

    public async Task<Usuario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni, Rol, 
                           Pais, Ciudad, Calle, Altura, FechaRegistro, Especialidad 
                    FROM Usuarios WHERE Id = @Id";

        var row = await connection.QuerySingleOrDefaultAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)
        );

        if (row is null)
            return null;

        // Construir los value objects
        Direccion direccion  = Direccion.CrearDireccion((string)row.Pais, (string)row.Ciudad, (string)row.Calle, (int)row.Altura);
        Email email      = Email.CrearEmail((string)row.Email);
        Contraseña contrasena = Contraseña.CrearContraseña((string)row.Contraseña);
        DNI dni        = DNI.CrearDNI((string)row.Dni);
        Roles rol        = Enum.Parse<Roles>((string)row.Rol);

        // Instanciar la clase concreta según el rol
        Usuario usuario = rol switch
        {
            Roles.Estudiante => new Estudiante(
                id:            (Guid)row.Id,
                direccion:     direccion,
                email:         email,
                contraseña:    contrasena,
                dni:           dni,
                nombre:        (string)row.Nombre,
                apellido:      (string)row.Apellido,
                fechaRegistro: (DateTime)row.FechaRegistro
            ),
            Roles.Profesor => new Profesor(
                id:            (Guid)row.Id,
                direccion:     direccion,
                email:         email,
                contraseña:    contrasena,
                dni:           dni,
                nombre:        (string)row.Nombre,
                apellido:      (string)row.Apellido,
                fechaRegistro: (DateTime)row.FechaRegistro,
                especialidad:  (string)row.Especialidad
            )
            
        };
        return usuario;
    }


    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = @"SELECT Id, Nombre, Apellido, Email, Contraseña, Dni, Rol, 
                           Pais, Ciudad, Calle, Altura, FechaRegistro, Especialidad 
                    FROM Usuarios";

        var rows = await connection.QueryAsync(sql);

        return rows.Select(row =>
        {
            Direccion direccion  = Direccion.CrearDireccion((string)row.Pais, (string)row.Ciudad, (string)row.Calle, (int)row.Altura);
            Email email          = Email.CrearEmail((string)row.Email);
            Contraseña contrasena = Contraseña.CrearContraseña((string)row.Contraseña);
            DNI dni              = DNI.CrearDNI((string)row.Dni);
            Roles rol            = Enum.Parse<Roles>((string)row.Rol);

            return rol switch
            {
                Roles.Estudiante => (Usuario)new Estudiante(
                    id:            (Guid)row.Id,
                    direccion:     direccion,
                    email:         email,
                    contraseña:    contrasena,
                    dni:           dni,
                    nombre:        (string)row.Nombre,
                    apellido:      (string)row.Apellido,
                    fechaRegistro: (DateTime)row.FechaRegistro
                ),
                Roles.Profesor => new Profesor(
                    id:            (Guid)row.Id,
                    direccion:     direccion,
                    email:         email,
                    contraseña:    contrasena,
                    dni:           dni,
                    nombre:        (string)row.Nombre,
                    apellido:      (string)row.Apellido,
                    fechaRegistro: (DateTime)row.FechaRegistro,
                    especialidad:  (string)row.Especialidad
                )
            };
        });
    }

    public async Task<UsuarioDTO?> VerDatosUsuario(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = "SELECT Id, Email, Nombre, Apellido FROM Usuarios WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<UsuarioDTO>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }
}
