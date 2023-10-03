using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fkToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "toDoModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_toDoModels_OwnerId",
                table: "toDoModels",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_toDoModels_AspNetUsers_OwnerId",
                table: "toDoModels",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_toDoModels_AspNetUsers_OwnerId",
                table: "toDoModels");

            migrationBuilder.DropIndex(
                name: "IX_toDoModels_OwnerId",
                table: "toDoModels");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "toDoModels");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
