using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineerApplication.ContextStructure.Migrations
{
    public partial class ImproveSupplierNamingConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumer",
                table: "Supplier",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Supplier",
                newName: "PhoneNumer");
        }
    }
}
