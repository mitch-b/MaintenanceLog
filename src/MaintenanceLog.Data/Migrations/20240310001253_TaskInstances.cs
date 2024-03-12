using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskInstances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "TaskTypes",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "TaskDefinitions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "TaskDefinitions",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Properties",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Assets",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Areas",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateTable(
                name: "TaskInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CompletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DueBy = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutomationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDefinitionId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskInstances_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskInstances_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskInstances_TaskDefinitions_TaskDefinitionId",
                        column: x => x.TaskDefinitionId,
                        principalTable: "TaskDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDefinitions_CreatedById",
                table: "TaskDefinitions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstances_AssignedToId",
                table: "TaskInstances",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstances_CreatedById",
                table: "TaskInstances",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstances_TaskDefinitionId",
                table: "TaskInstances",
                column: "TaskDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDefinitions_AspNetUsers_CreatedById",
                table: "TaskDefinitions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDefinitions_AspNetUsers_CreatedById",
                table: "TaskDefinitions");

            migrationBuilder.DropTable(
                name: "TaskInstances");

            migrationBuilder.DropIndex(
                name: "IX_TaskDefinitions_CreatedById",
                table: "TaskDefinitions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TaskTypes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TaskDefinitions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TaskDefinitions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Areas");
        }
    }
}
