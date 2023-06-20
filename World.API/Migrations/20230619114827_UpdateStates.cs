using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace World.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "population",
                table: "States",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "population",
                table: "States");
        }
    }
}
