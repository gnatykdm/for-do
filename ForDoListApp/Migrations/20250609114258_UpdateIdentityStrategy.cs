using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ForDoListApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityStrategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Tasks");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:task_category_enum", "work,personal,shopping,study,other")
                .Annotation("Npgsql:Enum:task_priority_enum", "low,medium,high")
                .Annotation("Npgsql:Enum:task_status_enum", "pending,in_progress,completed")
                .OldAnnotation("Npgsql:Enum:task_category_enum", "work,personal,shopping,study,other")
                .OldAnnotation("Npgsql:Enum:task_priority_enum", "low,medium,high")
                .OldAnnotation("Npgsql:Enum:task_status_enum", "pending,completed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:task_category_enum", "work,personal,shopping,study,other")
                .Annotation("Npgsql:Enum:task_priority_enum", "low,medium,high")
                .Annotation("Npgsql:Enum:task_status_enum", "pending,completed")
                .OldAnnotation("Npgsql:Enum:task_category_enum", "work,personal,shopping,study,other")
                .OldAnnotation("Npgsql:Enum:task_priority_enum", "low,medium,high")
                .OldAnnotation("Npgsql:Enum:task_status_enum", "pending,in_progress,completed");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskHistories",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ChangeDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistories", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_TaskId",
                table: "TaskHistories",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_UserId",
                table: "TaskHistories",
                column: "UserId");
        }
    }
}
