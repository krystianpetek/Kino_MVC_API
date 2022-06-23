using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPI.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NazwaSali",
                table: "SaleKinowe",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Klienci",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SaleKinowe_NazwaSali",
                table: "SaleKinowe",
                column: "NazwaSali",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_Login",
                table: "Login",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_Email",
                table: "Klienci",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SaleKinowe_NazwaSali",
                table: "SaleKinowe");

            migrationBuilder.DropIndex(
                name: "IX_Login_Login",
                table: "Login");

            migrationBuilder.DropIndex(
                name: "IX_Klienci_Email",
                table: "Klienci");

            migrationBuilder.AlterColumn<string>(
                name: "NazwaSali",
                table: "SaleKinowe",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Klienci",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
