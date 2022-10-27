using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
  public partial class ChangeTheStructureForCounting : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Commodity_Delivery_DeliveryId",
          table: "Commodity");

      migrationBuilder.DropForeignKey(
          name: "FK_Commodity_Frequency_FrequencyId",
          table: "Commodity");

      migrationBuilder.DropForeignKey(
          name: "FK_Commodity_Supplier_SupplierId",
          table: "Commodity");

      migrationBuilder.DropForeignKey(
          name: "FK_Service_Frequency_FrequencyId",
          table: "Service");

      migrationBuilder.DropTable(
          name: "Frequency");

      migrationBuilder.DropIndex(
          name: "IX_Commodity_DeliveryId",
          table: "Commodity");

      migrationBuilder.DropIndex(
          name: "IX_Commodity_FrequencyId",
          table: "Commodity");

      migrationBuilder.DropIndex(
          name: "IX_Commodity_SupplierId",
          table: "Commodity");

      migrationBuilder.DropColumn(
          name: "DeliveryId",
          table: "Commodity");

      migrationBuilder.DropColumn(
          name: "FrequencyId",
          table: "Commodity");

      migrationBuilder.DropColumn(
          name: "SupplierId",
          table: "Commodity");

      migrationBuilder.RenameColumn(
          name: "FrequencyId",
          table: "Service",
          newName: "PaymentId");

      migrationBuilder.RenameIndex(
          name: "IX_Service_FrequencyId",
          table: "Service",
          newName: "IX_Service_PaymentId");

      migrationBuilder.AddColumn<int>(
          name: "DeliveryId",
          table: "OrderHeader",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "PaymentId",
          table: "OrderHeader",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "SupplierId",
          table: "OrderHeader",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Payment",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Payment", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_OrderHeader_DeliveryId",
          table: "OrderHeader",
          column: "DeliveryId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderHeader_PaymentId",
          table: "OrderHeader",
          column: "PaymentId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderHeader_SupplierId",
          table: "OrderHeader",
          column: "SupplierId");

      migrationBuilder.AddForeignKey(
          name: "FK_OrderHeader_Delivery_DeliveryId",
          table: "OrderHeader",
          column: "DeliveryId",
          principalTable: "Delivery",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_OrderHeader_Payment_PaymentId",
          table: "OrderHeader",
          column: "PaymentId",
          principalTable: "Payment",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_OrderHeader_Supplier_SupplierId",
          table: "OrderHeader",
          column: "SupplierId",
          principalTable: "Supplier",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Service_Payment_PaymentId",
          table: "Service",
          column: "PaymentId",
          principalTable: "Payment",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_OrderHeader_Delivery_DeliveryId",
          table: "OrderHeader");

      migrationBuilder.DropForeignKey(
          name: "FK_OrderHeader_Payment_PaymentId",
          table: "OrderHeader");

      migrationBuilder.DropForeignKey(
          name: "FK_OrderHeader_Supplier_SupplierId",
          table: "OrderHeader");

      migrationBuilder.DropForeignKey(
          name: "FK_Service_Payment_PaymentId",
          table: "Service");

      migrationBuilder.DropTable(
          name: "Payment");

      migrationBuilder.DropIndex(
          name: "IX_OrderHeader_DeliveryId",
          table: "OrderHeader");

      migrationBuilder.DropIndex(
          name: "IX_OrderHeader_PaymentId",
          table: "OrderHeader");

      migrationBuilder.DropIndex(
          name: "IX_OrderHeader_SupplierId",
          table: "OrderHeader");

      migrationBuilder.DropColumn(
          name: "DeliveryId",
          table: "OrderHeader");

      migrationBuilder.DropColumn(
          name: "PaymentId",
          table: "OrderHeader");

      migrationBuilder.DropColumn(
          name: "SupplierId",
          table: "OrderHeader");

      migrationBuilder.RenameColumn(
          name: "PaymentId",
          table: "Service",
          newName: "FrequencyId");

      migrationBuilder.RenameIndex(
          name: "IX_Service_PaymentId",
          table: "Service",
          newName: "IX_Service_FrequencyId");

      migrationBuilder.AddColumn<int>(
          name: "DeliveryId",
          table: "Commodity",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "FrequencyId",
          table: "Commodity",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "SupplierId",
          table: "Commodity",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Frequency",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            FrequencyCount = table.Column<int>(type: "int", nullable: false),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Frequency", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Commodity_DeliveryId",
          table: "Commodity",
          column: "DeliveryId");

      migrationBuilder.CreateIndex(
          name: "IX_Commodity_FrequencyId",
          table: "Commodity",
          column: "FrequencyId");

      migrationBuilder.CreateIndex(
          name: "IX_Commodity_SupplierId",
          table: "Commodity",
          column: "SupplierId");

      migrationBuilder.AddForeignKey(
          name: "FK_Commodity_Delivery_DeliveryId",
          table: "Commodity",
          column: "DeliveryId",
          principalTable: "Delivery",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Commodity_Frequency_FrequencyId",
          table: "Commodity",
          column: "FrequencyId",
          principalTable: "Frequency",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Commodity_Supplier_SupplierId",
          table: "Commodity",
          column: "SupplierId",
          principalTable: "Supplier",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Service_Frequency_FrequencyId",
          table: "Service",
          column: "FrequencyId",
          principalTable: "Frequency",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
