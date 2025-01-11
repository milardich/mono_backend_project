using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_VehicleMakeId",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "VehicleMakeId",
                table: "VehicleModels",
                newName: "vehicleMakeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_VehicleMakeId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_vehicleMakeId");

            migrationBuilder.AlterColumn<int>(
                name: "vehicleMakeId",
                table: "VehicleModels",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_vehicleMakeId",
                table: "VehicleModels",
                column: "vehicleMakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_vehicleMakeId",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "vehicleMakeId",
                table: "VehicleModels",
                newName: "VehicleMakeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_vehicleMakeId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_VehicleMakeId");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleMakeId",
                table: "VehicleModels",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_VehicleMakeId",
                table: "VehicleModels",
                column: "VehicleMakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id");
        }
    }
}
