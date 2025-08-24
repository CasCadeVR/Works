using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasCadeVR.Works.Context.Migrations
{
    /// <inheritdoc />
    public partial class addedExecutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Executors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FIO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Occupation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Firm = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OGRN = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Executor_DeletedAt",
                table: "Executors",
                column: "FIO",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OGRN_DeletedAt",
                table: "Executors",
                column: "OGRN",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Executors");
        }
    }
}
