using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gatunek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgraniczeniaWiek = table.Column<int>(type: "int", nullable: false),
                    CzasTrwania = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RodzajUzytkownika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleKinowe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazwaSali = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IloscRzedow = table.Column<int>(type: "int", nullable: false),
                    IloscMiejsc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleKinowe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumerTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Miasto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UzytkownikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klienci_Login_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emisja",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Godzina = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emisja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emisja_Filmy_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emisja_SaleKinowe_SalaId",
                        column: x => x.SalaId,
                        principalTable: "SaleKinowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezerwacja",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rzad = table.Column<int>(type: "int", nullable: false),
                    Miejsce = table.Column<int>(type: "int", nullable: false),
                    EmisjaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezerwacja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezerwacja_Emisja_EmisjaId",
                        column: x => x.EmisjaId,
                        principalTable: "Emisja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezerwacja_Klienci_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Klienci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_FilmId",
                table: "Emisja",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Emisja_SalaId",
                table: "Emisja",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_Email",
                table: "Klienci",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Login_Login",
                table: "Login",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacja_EmisjaId",
                table: "Rezerwacja",
                column: "EmisjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacja_KlientId",
                table: "Rezerwacja",
                column: "KlientId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleKinowe_NazwaSali",
                table: "SaleKinowe",
                column: "NazwaSali",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezerwacja");

            migrationBuilder.DropTable(
                name: "Emisja");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Filmy");

            migrationBuilder.DropTable(
                name: "SaleKinowe");

            migrationBuilder.DropTable(
                name: "Login");
        }
    }
}
