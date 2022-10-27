using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class ImproveTheStatusOfRoleAndDatabaseModelClassDiagram : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Service_Delivery_DeliveryId",
          table: "Service");

      migrationBuilder.DropForeignKey(
          name: "FK_Service_Supplier_SupplierId",
          table: "Service");

      migrationBuilder.DropTable(
          name: "Offer");

      migrationBuilder.DropIndex(
          name: "IX_Service_DeliveryId",
          table: "Service");

      migrationBuilder.DropIndex(
          name: "IX_Service_SupplierId",
          table: "Service");

      migrationBuilder.DropColumn(
          name: "DeliveryId",
          table: "Service");

      migrationBuilder.DropColumn(
          name: "SupplierId",
          table: "Service");

      migrationBuilder.CreateTable(
          name: "Employee",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            EmployeeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
            ServiceId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Employee", x => x.Id);
            table.ForeignKey(
                      name: "FK_Employee_Service_ServiceId",
                      column: x => x.ServiceId,
                      principalTable: "Service",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Employee_ServiceId",
          table: "Employee",
          column: "ServiceId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Employee");

      migrationBuilder.AddColumn<int>(
          name: "DeliveryId",
          table: "Service",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "SupplierId",
          table: "Service",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Offer",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CommodityId = table.Column<int>(type: "int", nullable: false),
            Count = table.Column<int>(type: "int", nullable: false),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            OfferDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Offer", x => x.Id);
            table.ForeignKey(
                      name: "FK_Offer_Commodity_CommodityId",
                      column: x => x.CommodityId,
                      principalTable: "Commodity",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Service_DeliveryId",
          table: "Service",
          column: "DeliveryId");

      migrationBuilder.CreateIndex(
          name: "IX_Service_SupplierId",
          table: "Service",
          column: "SupplierId");

      migrationBuilder.CreateIndex(
          name: "IX_Offer_CommodityId",
          table: "Offer",
          column: "CommodityId");

      migrationBuilder.AddForeignKey(
          name: "FK_Service_Delivery_DeliveryId",
          table: "Service",
          column: "DeliveryId",
          principalTable: "Delivery",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Service_Supplier_SupplierId",
          table: "Service",
          column: "SupplierId",
          principalTable: "Supplier",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
