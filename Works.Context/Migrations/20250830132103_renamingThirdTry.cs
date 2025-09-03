using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasCadeVR.Works.Context.Migrations
{
    /// <inheritdoc />
    public partial class renamingThirdTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OGRN",
                table: "Executor",
                newName: "RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "FIO",
                table: "Executor",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "INN",
                table: "Customer",
                newName: "TaxPayerId");

            migrationBuilder.RenameColumn(
                name: "FIO",
                table: "Customer",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Executor",
                newName: "OGRN");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Executor",
                newName: "FIO");

            migrationBuilder.RenameColumn(
                name: "TaxPayerId",
                table: "Customer",
                newName: "INN");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customer",
                newName: "FIO");
        }
    }
}
