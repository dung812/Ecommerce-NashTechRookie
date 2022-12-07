using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Data.Migrations
{
    public partial class AddPropIsLockedFirstLoginInCustomerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLockedFirstLogin",
                table: "Customers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLockedFirstLogin",
                table: "Customers");
        }
    }
}
