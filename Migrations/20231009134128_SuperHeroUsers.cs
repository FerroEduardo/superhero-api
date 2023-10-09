using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class SuperHeroUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SuperHeroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SuperHeroes_UserId",
                table: "SuperHeroes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperHeroes_User_UserId",
                table: "SuperHeroes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperHeroes_User_UserId",
                table: "SuperHeroes");

            migrationBuilder.DropIndex(
                name: "IX_SuperHeroes_UserId",
                table: "SuperHeroes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SuperHeroes");
        }
    }
}
