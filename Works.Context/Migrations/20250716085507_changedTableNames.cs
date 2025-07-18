using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Works.Context.Migrations
{
    /// <inheritdoc />
    public partial class changedTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acts_Customers_CustomerId",
                table: "Acts");

            migrationBuilder.DropForeignKey(
                name: "FK_Acts_Executors_ExecutorId",
                table: "Acts");

            migrationBuilder.DropForeignKey(
                name: "FK_ActWorks_Acts_ActId",
                table: "ActWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_ActWorks_Works_WorkId",
                table: "ActWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Works",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executors",
                table: "Executors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActWorks",
                table: "ActWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acts",
                table: "Acts");

            migrationBuilder.RenameTable(
                name: "Works",
                newName: "Work");

            migrationBuilder.RenameTable(
                name: "Executors",
                newName: "Executor");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "ActWorks",
                newName: "ActWork");

            migrationBuilder.RenameTable(
                name: "Acts",
                newName: "Act");

            migrationBuilder.RenameIndex(
                name: "IX_ActWorks_ActId",
                table: "ActWork",
                newName: "IX_ActWork_ActId");

            migrationBuilder.RenameIndex(
                name: "IX_Acts_ExecutorId",
                table: "Act",
                newName: "IX_Act_ExecutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Acts_CustomerId",
                table: "Act",
                newName: "IX_Act_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Work",
                table: "Work",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executor",
                table: "Executor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActWork",
                table: "ActWork",
                columns: new[] { "WorkId", "ActId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Act",
                table: "Act",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Act_Customer_CustomerId",
                table: "Act",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Act_Executor_ExecutorId",
                table: "Act",
                column: "ExecutorId",
                principalTable: "Executor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActWork_Act_ActId",
                table: "ActWork",
                column: "ActId",
                principalTable: "Act",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActWork_Work_WorkId",
                table: "ActWork",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Act_Customer_CustomerId",
                table: "Act");

            migrationBuilder.DropForeignKey(
                name: "FK_Act_Executor_ExecutorId",
                table: "Act");

            migrationBuilder.DropForeignKey(
                name: "FK_ActWork_Act_ActId",
                table: "ActWork");

            migrationBuilder.DropForeignKey(
                name: "FK_ActWork_Work_WorkId",
                table: "ActWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Work",
                table: "Work");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executor",
                table: "Executor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActWork",
                table: "ActWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Act",
                table: "Act");

            migrationBuilder.RenameTable(
                name: "Work",
                newName: "Works");

            migrationBuilder.RenameTable(
                name: "Executor",
                newName: "Executors");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "ActWork",
                newName: "ActWorks");

            migrationBuilder.RenameTable(
                name: "Act",
                newName: "Acts");

            migrationBuilder.RenameIndex(
                name: "IX_ActWork_ActId",
                table: "ActWorks",
                newName: "IX_ActWorks_ActId");

            migrationBuilder.RenameIndex(
                name: "IX_Act_ExecutorId",
                table: "Acts",
                newName: "IX_Acts_ExecutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Act_CustomerId",
                table: "Acts",
                newName: "IX_Acts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Works",
                table: "Works",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executors",
                table: "Executors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActWorks",
                table: "ActWorks",
                columns: new[] { "WorkId", "ActId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acts",
                table: "Acts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acts_Customers_CustomerId",
                table: "Acts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Acts_Executors_ExecutorId",
                table: "Acts",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActWorks_Acts_ActId",
                table: "ActWorks",
                column: "ActId",
                principalTable: "Acts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActWorks_Works_WorkId",
                table: "ActWorks",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
