using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Steps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskDefinitionSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: true),
                    TaskDefinitionId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDefinitionSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskDefinitionSteps_TaskDefinitions_TaskDefinitionId",
                        column: x => x.TaskDefinitionId,
                        principalTable: "TaskDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskInstanceSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CompletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TaskInstanceId = table.Column<int>(type: "int", nullable: true),
                    TaskDefinitionStepId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstanceSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskInstanceSteps_AspNetUsers_CompletedById",
                        column: x => x.CompletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskInstanceSteps_TaskDefinitionSteps_TaskDefinitionStepId",
                        column: x => x.TaskDefinitionStepId,
                        principalTable: "TaskDefinitionSteps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskInstanceSteps_TaskInstances_TaskInstanceId",
                        column: x => x.TaskInstanceId,
                        principalTable: "TaskInstances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDefinitionSteps_TaskDefinitionId",
                table: "TaskDefinitionSteps",
                column: "TaskDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstanceSteps_CompletedById",
                table: "TaskInstanceSteps",
                column: "CompletedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstanceSteps_TaskDefinitionStepId",
                table: "TaskInstanceSteps",
                column: "TaskDefinitionStepId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstanceSteps_TaskInstanceId",
                table: "TaskInstanceSteps",
                column: "TaskInstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskInstanceSteps");

            migrationBuilder.DropTable(
                name: "TaskDefinitionSteps");
        }
    }
}
