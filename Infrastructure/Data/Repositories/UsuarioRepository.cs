using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

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
        Roles rol        = Enum.Parse<Roles>((string)row.Rol);

        // Instanciar la clase concreta según el rol
        Usuario usuario = rol switch
        {
            Roles.Estudiante => Estudiante.ReconstruirEstudiante(row),
            Roles.Profesor => Profesor.Reconstruir(
                id:            (Guid)row.Id,
                pais:       (string)row.Pais,
                ciudad:     (string)row.Ciudad,
                calle:      (string)row.Calle,
                altura:     (int)row.Altura,
                email:      (string)row.Email,
                contraseña:    row.Contraseña,
                dni:           row.DNI,
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

        var result_rows = rows.Select(row =>
        {
            Roles rol            = Enum.Parse<Roles>((string)row.Rol);

            Usuario user = rol switch
            {
                Roles.Estudiante => Estudiante.ReconstruirEstudiante(row),
                Roles.Profesor => Profesor.Reconstruir(
                    id:            (Guid)row.Id,
                    pais:       (string)row.Pais,
                    ciudad:     (string)row.Ciudad,
                    calle:      (string)row.Calle,
                    altura:     (int)row.Altura,
                    email:      (string)row.Email,
                    contraseña:    row.Contraseña,
                    dni:           row.Dni,
                    nombre:        (string)row.Nombre,
                    apellido:      (string)row.Apellido,
                    fechaRegistro: (DateTime)row.FechaRegistro,
                    especialidad:  (string)row.Especialidad
                )
            };
            return user;
        });
        
        return result_rows.Cast<Usuario>();
    }

    public async Task<UsuarioDTO?> VerDatosUsuario(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = DbContext.Database.GetDbConnection();
        var sql = "SELECT Id, Email, Nombre, Apellido FROM Usuarios WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<UsuarioDTO>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }
}
