using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AreaAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AreaId",
                table: "Assets",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Areas_AreaId",
                table: "Assets",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Areas_AreaId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AreaId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "QrCode",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Assets");
        }
    }
}
