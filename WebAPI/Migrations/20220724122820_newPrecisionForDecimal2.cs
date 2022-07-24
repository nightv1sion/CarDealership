using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class newPrecisionForDecimal2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "PhotosForDealershop",
                type: "decimal(30,10)",
                precision: 30,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,30)",
                oldPrecision: 30,
                oldScale: 30);

            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "PhotosForCar",
                type: "decimal(30,10)",
                precision: 30,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,30)",
                oldPrecision: 30,
                oldScale: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "PhotosForDealershop",
                type: "decimal(30,30)",
                precision: 30,
                scale: 30,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)",
                oldPrecision: 30,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "PhotosForCar",
                type: "decimal(30,30)",
                precision: 30,
                scale: 30,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)",
                oldPrecision: 30,
                oldScale: 10);
        }
    }
}
