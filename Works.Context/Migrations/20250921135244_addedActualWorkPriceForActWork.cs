using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasCadeVR.Works.Context.Migrations
{
    /// <inheritdoc />
    public partial class addedActualWorkPriceForActWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualWorkPrice",
                table: "ActWork",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualWorkPrice",
                table: "ActWork");
        }
    }
}
