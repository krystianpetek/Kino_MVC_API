using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAPI.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezerwacja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zajete = table.Column<bool>(type: "bit", nullable: false),
                    Rzad = table.Column<int>(type: "int", nullable: false),
                    Miejsce = table.Column<int>(type: "int", nullable: false),
                    LiczbaPorzadkowa = table.Column<int>(type: "int", nullable: false),
                    GodzinaEmisji = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdSaleKinowe = table.Column<int>(type: "int", nullable: false),
                    IdFilm = table.Column<int>(type: "int", nullable: false),
                    IdKlient = table.Column<int>(type: "int", nullable: false),
                    SaleKinoweId = table.Column<int>(type: "int", nullable: true),
                    FilmyId = table.Column<int>(type: "int", nullable: true),
                    KlienciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezerwacja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezerwacja_Filmy_FilmyId",
                        column: x => x.FilmyId,
                        principalTable: "Filmy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezerwacja_Klienci_KlienciId",
                        column: x => x.KlienciId,
                        principalTable: "Klienci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezerwacja_SaleKinowe_SaleKinoweId",
                        column: x => x.SaleKinoweId,
                        principalTable: "SaleKinowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacja_FilmyId",
                table: "Rezerwacja",
                column: "FilmyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacja_KlienciId",
                table: "Rezerwacja",
                column: "KlienciId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacja_SaleKinoweId",
                table: "Rezerwacja",
                column: "SaleKinoweId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezerwacja");
        }
    }
}
