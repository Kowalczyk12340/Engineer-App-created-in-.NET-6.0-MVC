using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class ImproveModels : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<byte[]>(
          name: "Picture",
          table: "WebImages",
          type: "varbinary(max)",
          nullable: true,
          oldClrType: typeof(byte[]),
          oldType: "varbinary(max)");

      migrationBuilder.AlterColumn<string>(
          name: "Status",
          table: "OrderHeader",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      migrationBuilder.AlterColumn<string>(
          name: "Comments",
          table: "OrderHeader",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      migrationBuilder.AlterColumn<string>(
          name: "LongDesc",
          table: "Commodity",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      migrationBuilder.AlterColumn<string>(
          name: "ImageUrl",
          table: "Commodity",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<byte[]>(
          name: "Picture",
          table: "WebImages",
          type: "varbinary(max)",
          nullable: false,
          defaultValue: new byte[0],
          oldClrType: typeof(byte[]),
          oldType: "varbinary(max)",
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "Status",
          table: "OrderHeader",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(max)",
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "Comments",
          table: "OrderHeader",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(max)",
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "LongDesc",
          table: "Commodity",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(max)",
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "ImageUrl",
          table: "Commodity",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(max)",
          oldNullable: true);
    }
  }
}
