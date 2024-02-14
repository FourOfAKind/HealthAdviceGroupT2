using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthAdviceGroup.Migrations
{
    /// <inheritdoc />
    public partial class saveInit4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Save",
                newName: "AdviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdviceId",
                table: "Save",
                newName: "CourseId");
        }
    }
}
