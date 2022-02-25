using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
    public partial class AddSupplierEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Commodity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commodity_SupplierId",
                table: "Commodity",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commodity_Supplier_SupplierId",
                table: "Commodity",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commodity_Supplier_SupplierId",
                table: "Commodity");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Commodity_SupplierId",
                table: "Commodity");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Commodity");
        }
    }
}
