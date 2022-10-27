using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class AddOfferEntity : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<int>(
          name: "OfferId",
          table: "Commodity",
          type: "int",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "Offer",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Offer", x => x.Id);
          });

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

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Commodity_Offer_OfferId",
          table: "Commodity");

      migrationBuilder.DropTable(
          name: "Offer");

      migrationBuilder.DropIndex(
          name: "IX_Commodity_OfferId",
          table: "Commodity");

      migrationBuilder.DropColumn(
          name: "OfferId",
          table: "Commodity");
    }
  }
}
