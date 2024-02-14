using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthAdviceGroup.Migrations
{
    /// <inheritdoc />
    public partial class adviceInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Advice",
                type: "float",
                nullable: true,
                defaultValue: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Advice");
        }
    }
}
