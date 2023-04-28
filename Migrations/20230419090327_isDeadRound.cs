using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWars.Migrations
{
    /// <inheritdoc />
    public partial class isDeadRound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Rounds",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Rounds");
        }
    }
}
