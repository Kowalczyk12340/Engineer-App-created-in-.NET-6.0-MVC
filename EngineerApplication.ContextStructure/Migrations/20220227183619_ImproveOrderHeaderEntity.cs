using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class ImproveOrderHeaderEntity : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
          name: "Count",
          table: "OrderHeader",
          newName: "CommodityCount");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
          name: "CommodityCount",
          table: "OrderHeader",
          newName: "Count");
    }
  }
}
