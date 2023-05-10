using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tomates_podridos.Migrations
{
    public partial class si : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelicula_ComentarioAudiencia_ComentarioAudienciaId",
                table: "Pelicula");

            migrationBuilder.DropForeignKey(
                name: "FK_Pelicula_ComentarioCritica_ComentarioCriticaId",
                table: "Pelicula");

            migrationBuilder.DropIndex(
                name: "IX_Pelicula_ComentarioAudienciaId",
                table: "Pelicula");

            migrationBuilder.DropIndex(
                name: "IX_Pelicula_ComentarioCriticaId",
                table: "Pelicula");

            migrationBuilder.DropColumn(
                name: "ComentarioAudienciaId",
                table: "Pelicula");

            migrationBuilder.DropColumn(
                name: "ComentarioCriticaId",
                table: "Pelicula");

            migrationBuilder.AddColumn<int>(
                name: "PeliculaId",
                table: "ComentarioCritica",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeliculaId",
                table: "ComentarioAudiencia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioCritica_PeliculaId",
                table: "ComentarioCritica",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioAudiencia_PeliculaId",
                table: "ComentarioAudiencia",
                column: "PeliculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComentarioAudiencia_Pelicula_PeliculaId",
                table: "ComentarioAudiencia",
                column: "PeliculaId",
                principalTable: "Pelicula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComentarioCritica_Pelicula_PeliculaId",
                table: "ComentarioCritica",
                column: "PeliculaId",
                principalTable: "Pelicula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComentarioAudiencia_Pelicula_PeliculaId",
                table: "ComentarioAudiencia");

            migrationBuilder.DropForeignKey(
                name: "FK_ComentarioCritica_Pelicula_PeliculaId",
                table: "ComentarioCritica");

            migrationBuilder.DropIndex(
                name: "IX_ComentarioCritica_PeliculaId",
                table: "ComentarioCritica");

            migrationBuilder.DropIndex(
                name: "IX_ComentarioAudiencia_PeliculaId",
                table: "ComentarioAudiencia");

            migrationBuilder.DropColumn(
                name: "PeliculaId",
                table: "ComentarioCritica");

            migrationBuilder.DropColumn(
                name: "PeliculaId",
                table: "ComentarioAudiencia");

            migrationBuilder.AddColumn<int>(
                name: "ComentarioAudienciaId",
                table: "Pelicula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComentarioCriticaId",
                table: "Pelicula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_ComentarioAudienciaId",
                table: "Pelicula",
                column: "ComentarioAudienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_ComentarioCriticaId",
                table: "Pelicula",
                column: "ComentarioCriticaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelicula_ComentarioAudiencia_ComentarioAudienciaId",
                table: "Pelicula",
                column: "ComentarioAudienciaId",
                principalTable: "ComentarioAudiencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelicula_ComentarioCritica_ComentarioCriticaId",
                table: "Pelicula",
                column: "ComentarioCriticaId",
                principalTable: "ComentarioCritica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
