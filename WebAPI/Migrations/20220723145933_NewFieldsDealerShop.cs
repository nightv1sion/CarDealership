using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class NewFieldsDealerShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DealerShopId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DealerShops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "DealerShops",
                type: "geography",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "DealerShops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_DealerShopId",
                table: "Photos",
                column: "DealerShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_DealerShops_DealerShopId",
                table: "Photos",
                column: "DealerShopId",
                principalTable: "DealerShops",
                principalColumn: "DealerShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_DealerShops_DealerShopId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_DealerShopId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DealerShopId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "DealerShops");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "DealerShops");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "DealerShops");
        }
    }
}
