using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPI.Migrations
{
    public partial class initializer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja");

            migrationBuilder.CreateTable(
                name: "ZajeteMiejsca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmisjaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rzad = table.Column<int>(type: "int", nullable: false),
                    Miejsce = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZajeteMiejsca", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleKinowe_Id",
                table: "SaleKinowe",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacja_Id",
                table: "Rezerwacja",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_Id",
                table: "Login",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_Id",
                table: "Klienci",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci",
                column: "UzytkownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filmy_Id",
                table: "Filmy",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja",
                column: "FilmId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_Id",
                table: "Emisja",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja",
                column: "SalaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZajeteMiejsca_Id",
                table: "ZajeteMiejsca",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZajeteMiejsca");

            migrationBuilder.DropIndex(
                name: "IX_SaleKinowe_Id",
                table: "SaleKinowe");

            migrationBuilder.DropIndex(
                name: "IX_Rezerwacja_Id",
                table: "Rezerwacja");

            migrationBuilder.DropIndex(
                name: "IX_Login_Id",
                table: "Login");

            migrationBuilder.DropIndex(
                name: "IX_Klienci_Id",
                table: "Klienci");

            migrationBuilder.DropIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci");

            migrationBuilder.DropIndex(
                name: "IX_Filmy_Id",
                table: "Filmy");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_Id",
                table: "Emisja");

            migrationBuilder.DropIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja");

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja",
                column: "SalaId");
        }
    }
}
