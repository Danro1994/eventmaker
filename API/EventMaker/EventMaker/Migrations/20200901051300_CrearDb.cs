using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventMaker.Migrations
{
    public partial class CrearDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoriaEventos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    categoria_evento = table.Column<string>(nullable: true),
                    icono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoriaEventos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invitados",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre_invitado = table.Column<string>(nullable: true),
                    apellido_invitado = table.Column<string>(nullable: true),
                    descripcion = table.Column<string>(nullable: true),
                    url_image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invitados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre_usuario = table.Column<string>(nullable: true),
                    apellido_usuario = table.Column<string>(nullable: true),
                    edad = table.Column<int>(nullable: false),
                    correo_electronico = table.Column<string>(nullable: true),
                    fecha_Registro = table.Column<DateTime>(nullable: false),
                    pases_articulos = table.Column<string>(nullable: true),
                    talleres_registrados = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reservacions",
                columns: table => new
                {
                    id = table.Column<int>(maxLength: 50, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    usuarioid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservacions", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservacions_usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre_evento = table.Column<string>(nullable: true),
                    lugar = table.Column<string>(nullable: true),
                    precio = table.Column<decimal>(nullable: false),
                    fecha = table.Column<DateTime>(nullable: false),
                    clave = table.Column<string>(nullable: true),
                    categoriaEventoid = table.Column<int>(nullable: false),
                    invitadoid = table.Column<int>(nullable: false),
                    reservacionid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos", x => x.id);
                    table.ForeignKey(
                        name: "FK_eventos_categoriaEventos_categoriaEventoid",
                        column: x => x.categoriaEventoid,
                        principalTable: "categoriaEventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventos_invitados_invitadoid",
                        column: x => x.invitadoid,
                        principalTable: "invitados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventos_reservacions_reservacionid",
                        column: x => x.reservacionid,
                        principalTable: "reservacions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventos_categoriaEventoid",
                table: "eventos",
                column: "categoriaEventoid");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_invitadoid",
                table: "eventos",
                column: "invitadoid");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_reservacionid",
                table: "eventos",
                column: "reservacionid");

            migrationBuilder.CreateIndex(
                name: "IX_reservacions_usuarioid",
                table: "reservacions",
                column: "usuarioid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventos");

            migrationBuilder.DropTable(
                name: "categoriaEventos");

            migrationBuilder.DropTable(
                name: "invitados");

            migrationBuilder.DropTable(
                name: "reservacions");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
