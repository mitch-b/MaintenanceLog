using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskDefinitionArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDefinitions_Assets_AssetId",
                table: "TaskDefinitions");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "TaskDefinitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "TaskDefinitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskDefinitions_AreaId",
                table: "TaskDefinitions",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDefinitions_Areas_AreaId",
                table: "TaskDefinitions",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDefinitions_Assets_AssetId",
                table: "TaskDefinitions",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDefinitions_Areas_AreaId",
                table: "TaskDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDefinitions_Assets_AssetId",
                table: "TaskDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_TaskDefinitions_AreaId",
                table: "TaskDefinitions");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "TaskDefinitions");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "TaskDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDefinitions_Assets_AssetId",
                table: "TaskDefinitions",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
