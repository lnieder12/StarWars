using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWars.Migrations
{
    /// <inheritdoc />
    public partial class Hpleft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HpLeft",
                table: "Rounds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HpLeft",
                table: "Rounds");
        }
    }
}
