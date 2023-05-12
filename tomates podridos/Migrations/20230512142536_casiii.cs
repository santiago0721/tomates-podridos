using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tomates_podridos.Migrations
{
    public partial class casiii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "topsPelicula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topsPelicula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topsPelicula_Pelicula_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topsShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topsShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topsShows_Show_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_topsPelicula_PeliculaId",
                table: "topsPelicula",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_topsShows_ShowId",
                table: "topsShows",
                column: "ShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "topsPelicula");

            migrationBuilder.DropTable(
                name: "topsShows");
        }
    }
}
