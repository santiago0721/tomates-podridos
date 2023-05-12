using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tomates_podridos.Migrations
{
    public partial class casilisto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calCritica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calAudiencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plataformas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    produccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actores = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioAudiencia_show",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioAudiencia_show", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentarioAudiencia_show_Show_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioCritica_show",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioCritica_show", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentarioCritica_show_Show_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioAudiencia_show_ShowId",
                table: "ComentarioAudiencia_show",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioCritica_show_ShowId",
                table: "ComentarioCritica_show",
                column: "ShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentarioAudiencia_show");

            migrationBuilder.DropTable(
                name: "ComentarioCritica_show");

            migrationBuilder.DropTable(
                name: "Show");
        }
    }
}
