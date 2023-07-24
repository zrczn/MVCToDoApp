using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "toDoModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RoutineOption = table.Column<int>(type: "int", nullable: false),
                    ShowAtSingleDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toDoModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "audioModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDoModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audioModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_audioModels_toDoModels_ToDoModelId",
                        column: x => x.ToDoModelId,
                        principalTable: "toDoModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "photoModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDoModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photoModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photoModels_toDoModels_ToDoModelId",
                        column: x => x.ToDoModelId,
                        principalTable: "toDoModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audioModels_ToDoModelId",
                table: "audioModels",
                column: "ToDoModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_photoModels_ToDoModelId",
                table: "photoModels",
                column: "ToDoModelId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audioModels");

            migrationBuilder.DropTable(
                name: "photoModels");

            migrationBuilder.DropTable(
                name: "toDoModels");
        }
    }
}
