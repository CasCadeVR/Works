using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasCadeVR.Works.Context.Migrations
{
    /// <inheritdoc />
    public partial class massiveChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Executor_DeletedAt",
                table: "Executor");

            migrationBuilder.DropIndex(
                name: "IX_Customer_DeletedAt",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActWork",
                table: "ActWork");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "ActualPrice",
                table: "ActWork");

            migrationBuilder.DropColumn(
                name: "NDS",
                table: "Act");

            migrationBuilder.RenameIndex(
                name: "IX_Work_DeletedAt",
                table: "Work",
                newName: "IX_Work_Name");

            migrationBuilder.RenameIndex(
                name: "IX_OGRN_DeletedAt",
                table: "Executor",
                newName: "IX_Executor_RegistrationNumber");

            migrationBuilder.RenameIndex(
                name: "IX_INN_DeletedAt",
                table: "Customer",
                newName: "IX_Customer_TaxPayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Act_DeletedAt",
                table: "Act",
                newName: "IX_Act_ActNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Work",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasureId",
                table: "Work",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "OGRN",
                table: "Executor",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "INN",
                table: "Customer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ActWork",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "ActWork",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "ActWork",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "ActWork",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActWork",
                table: "ActWork",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Work_UnitOfMeasureId",
                table: "Work",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ActWork_WorkId",
                table: "ActWork",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_Name",
                table: "UnitOfMeasure",
                column: "Name",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_UnitOfMeasure_UnitOfMeasureId",
                table: "Work",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_UnitOfMeasure_UnitOfMeasureId",
                table: "Work");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropIndex(
                name: "IX_Work_UnitOfMeasureId",
                table: "Work");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActWork",
                table: "ActWork");

            migrationBuilder.DropIndex(
                name: "IX_ActWork_WorkId",
                table: "ActWork");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActWork");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ActWork");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ActWork");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ActWork");

            migrationBuilder.RenameIndex(
                name: "IX_Work_Name",
                table: "Work",
                newName: "IX_Work_DeletedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Executor_RegistrationNumber",
                table: "Executor",
                newName: "IX_OGRN_DeletedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_TaxPayerId",
                table: "Customer",
                newName: "IX_INN_DeletedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Act_ActNumber",
                table: "Act",
                newName: "IX_Act_DeletedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Work",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasure",
                table: "Work",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Executor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "TaxPayerId",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualPrice",
                table: "ActWork",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AddedTax",
                table: "Act",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActWork",
                table: "ActWork",
                columns: new[] { "WorkId", "ActId" });

            migrationBuilder.CreateIndex(
                name: "IX_Executor_DeletedAt",
                table: "Executor",
                column: "FullName",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeletedAt",
                table: "Customer",
                column: "FullName",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }
    }
}
