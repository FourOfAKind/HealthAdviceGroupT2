using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthAdviceGroup.Migrations
{
    /// <inheritdoc />
    public partial class healthInit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Health",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Health");
        }
    }
}
