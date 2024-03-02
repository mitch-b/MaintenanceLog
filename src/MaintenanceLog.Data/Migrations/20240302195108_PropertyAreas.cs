using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class PropertyAreas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_PropertyId",
                table: "Areas",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Properties_PropertyId",
                table: "Areas",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Properties_PropertyId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_PropertyId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Areas");
        }
    }
}
