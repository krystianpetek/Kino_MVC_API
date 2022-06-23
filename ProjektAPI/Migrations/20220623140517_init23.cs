using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPI.Migrations
{
    public partial class init23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja");

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja",
                column: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja");

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja",
                column: "FilmId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja",
                column: "SalaId",
                unique: true);
        }
    }
}
