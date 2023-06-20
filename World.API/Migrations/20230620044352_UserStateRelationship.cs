using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace World.API.Migrations
{
    /// <inheritdoc />
    public partial class UserStateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "States",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_UserId",
                table: "States",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Users_UserId",
                table: "States",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_UserId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_UserId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "States");
        }
    }
}
