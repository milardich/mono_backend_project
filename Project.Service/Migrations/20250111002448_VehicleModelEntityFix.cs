using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class VehicleModelEntityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MakeId",
                table: "VehicleModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MakeId",
                table: "VehicleModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
