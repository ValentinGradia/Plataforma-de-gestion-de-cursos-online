using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Ciudad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Calle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Altura = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Contrasea = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Dni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Rol = table.Column<string>(type: "text", nullable: false),
                    Especialidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProfesor = table.Column<Guid>(type: "uuid", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Temario = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Usuarios_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCurso = table.Column<Guid>(type: "uuid", nullable: false),
                    Material = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clases_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCurso = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEstudiante = table.Column<Guid>(type: "uuid", nullable: true),
                    CalificacionCurso = table.Column<int>(type: "integer", nullable: false),
                    CalificacionMaterial = table.Column<int>(type: "integer", nullable: false),
                    CalificacionDocente = table.Column<int>(type: "integer", nullable: false),
                    Comentarios = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encuestas_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encuestas_Usuarios_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Examenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCurso = table.Column<Guid>(type: "uuid", nullable: false),
                    TemaExamen = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FechaLimiteDeEntrega = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaExamenCargado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examenes_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEstudiante = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCurso = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    PorcentajeAsistencia = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Usuarios_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClase = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEstudiante = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    FechaConsulta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Clases_IdClase",
                        column: x => x.IdClase,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Usuarios_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClase = table.Column<Guid>(type: "uuid", nullable: false),
                    IdInscripcionEstudiante = table.Column<Guid>(type: "uuid", nullable: false),
                    Presente = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencias_Clases_IdClase",
                        column: x => x.IdClase,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencias_Inscripciones_IdInscripcionEstudiante",
                        column: x => x.IdInscripcionEstudiante,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntregasExamen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdExamen = table.Column<Guid>(type: "uuid", nullable: false),
                    IdInscripcionEstudiante = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Respuesta = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Nota = table.Column<decimal>(type: "numeric", nullable: true),
                    FechaEntregado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ComentarioDocente = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregasExamen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregasExamen_Examenes_IdExamen",
                        column: x => x.IdExamen,
                        principalTable: "Examenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntregasExamen_Inscripciones_IdInscripcionEstudiante",
                        column: x => x.IdInscripcionEstudiante,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdClase",
                table: "Asistencias",
                column: "IdClase");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdInscripcionEstudiante",
                table: "Asistencias",
                column: "IdInscripcionEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Clases_IdCurso",
                table: "Clases",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IdClase",
                table: "Consultas",
                column: "IdClase");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IdEstudiante",
                table: "Consultas",
                column: "IdEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_IdProfesor",
                table: "Cursos",
                column: "IdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_IdCurso",
                table: "Encuestas",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_IdEstudiante",
                table: "Encuestas",
                column: "IdEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasExamen_IdExamen",
                table: "EntregasExamen",
                column: "IdExamen");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasExamen_IdInscripcionEstudiante",
                table: "EntregasExamen",
                column: "IdInscripcionEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_IdCurso",
                table: "Examenes",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdCurso",
                table: "Inscripciones",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdEstudiante",
                table: "Inscripciones",
                column: "IdEstudiante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "EntregasExamen");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "Examenes");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
