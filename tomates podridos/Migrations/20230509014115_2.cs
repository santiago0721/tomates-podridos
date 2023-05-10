using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tomates_podridos.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComentarioAudiencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioAudiencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioCritica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioCritica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calCritica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calAudiencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plataformas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clasificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipoDir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duracion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actores = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    generoId = table.Column<int>(type: "int", nullable: false),
                    ComentarioAudienciaId = table.Column<int>(type: "int", nullable: false),
                    ComentarioCriticaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pelicula_ComentarioAudiencia_ComentarioAudienciaId",
                        column: x => x.ComentarioAudienciaId,
                        principalTable: "ComentarioAudiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelicula_ComentarioCritica_ComentarioCriticaId",
                        column: x => x.ComentarioCriticaId,
                        principalTable: "ComentarioCritica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelicula_genero_generoId",
                        column: x => x.generoId,
                        principalTable: "genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_ComentarioAudienciaId",
                table: "Pelicula",
                column: "ComentarioAudienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_ComentarioCriticaId",
                table: "Pelicula",
                column: "ComentarioCriticaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_generoId",
                table: "Pelicula",
                column: "generoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "ComentarioAudiencia");

            migrationBuilder.DropTable(
                name: "ComentarioCritica");

            migrationBuilder.DropTable(
                name: "genero");
        }
    }
}
