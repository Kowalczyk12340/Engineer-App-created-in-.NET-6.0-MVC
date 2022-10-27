using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class AddTheDateTimeToOrder : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<DateTime>(
          name: "TimeToOrder",
          table: "OrderHeader",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.AddColumn<DateTime>(
          name: "TimeToRealisation",
          table: "OrderHeader",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "TimeToOrder",
          table: "OrderHeader");

      migrationBuilder.DropColumn(
          name: "TimeToRealisation",
          table: "OrderHeader");
    }
  }
}
