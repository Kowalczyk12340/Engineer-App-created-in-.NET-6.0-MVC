using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class AddAmountPropToCommodity : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Amount",
          table: "OrderHeader");

      migrationBuilder.AddColumn<int>(
          name: "Amount",
          table: "Commodity",
          type: "int",
          nullable: false,
          defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Amount",
          table: "Commodity");

      migrationBuilder.AddColumn<int>(
          name: "Amount",
          table: "OrderHeader",
          type: "int",
          nullable: false,
          defaultValue: 0);
    }
  }
}
