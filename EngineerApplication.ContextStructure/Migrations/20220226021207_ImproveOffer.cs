using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class ImproveOffer : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Commodity_Offer_OfferId",
          table: "Commodity");

      migrationBuilder.DropIndex(
          name: "IX_Commodity_OfferId",
          table: "Commodity");

      migrationBuilder.DropColumn(
          name: "OfferId",
          table: "Commodity");

      migrationBuilder.AddColumn<int>(
          name: "Count",
          table: "Offer",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<string>(
          name: "OfferDesc",
          table: "Offer",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Count",
          table: "Offer");

      migrationBuilder.DropColumn(
          name: "OfferDesc",
          table: "Offer");

      migrationBuilder.AddColumn<int>(
          name: "OfferId",
          table: "Commodity",
          type: "int",
          nullable: true);

      migrationBuilder.CreateIndex(
          name: "IX_Commodity_OfferId",
          table: "Commodity",
          column: "OfferId");

      migrationBuilder.AddForeignKey(
          name: "FK_Commodity_Offer_OfferId",
          table: "Commodity",
          column: "OfferId",
          principalTable: "Offer",
          principalColumn: "Id");
    }
  }
}
