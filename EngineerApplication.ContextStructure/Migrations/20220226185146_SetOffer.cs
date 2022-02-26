using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
    public partial class SetOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommodityId",
                table: "Offer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offer_CommodityId",
                table: "Offer",
                column: "CommodityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Commodity_CommodityId",
                table: "Offer",
                column: "CommodityId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Commodity_CommodityId",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_CommodityId",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "CommodityId",
                table: "Offer");
        }
    }
}
