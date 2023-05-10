using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tomates_podridos.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelicula_genero_generoId",
                table: "Pelicula");

            migrationBuilder.DropIndex(
                name: "IX_Pelicula_generoId",
                table: "Pelicula");

            migrationBuilder.DropColumn(
                name: "generoId",
                table: "Pelicula");

            migrationBuilder.CreateTable(
                name: "Peliculagenero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    generoId = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculagenero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peliculagenero_genero_generoId",
                        column: x => x.generoId,
                        principalTable: "genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peliculagenero_Pelicula_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peliculagenero_generoId",
                table: "Peliculagenero",
                column: "generoId");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculagenero_PeliculaId",
                table: "Peliculagenero",
                column: "PeliculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peliculagenero");

            migrationBuilder.AddColumn<int>(
                name: "generoId",
                table: "Pelicula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_generoId",
                table: "Pelicula",
                column: "generoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelicula_genero_generoId",
                table: "Pelicula",
                column: "generoId",
                principalTable: "genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
