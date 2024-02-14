using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthAdviceGroup.Migrations
{
    /// <inheritdoc />
    public partial class saveInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Save");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Save",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Save",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Save");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Save",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Save",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
