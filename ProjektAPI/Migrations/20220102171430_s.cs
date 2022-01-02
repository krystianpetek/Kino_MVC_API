﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAPI.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Godzina",
                table: "Emisja",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci",
                column: "UzytkownikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Godzina",
                table: "Emisja",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_UzytkownikId",
                table: "Klienci",
                column: "UzytkownikId",
                unique: true);
        }
    }
}