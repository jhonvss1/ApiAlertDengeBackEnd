using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAlertaDengue.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "EstimatedCases",
                table: "DengueAlerts",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EstimatedCases",
                table: "DengueAlerts",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
