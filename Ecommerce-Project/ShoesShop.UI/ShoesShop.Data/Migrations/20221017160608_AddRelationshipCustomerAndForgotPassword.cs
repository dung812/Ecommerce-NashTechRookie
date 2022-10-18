using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Data.Migrations
{
    public partial class AddRelationshipCustomerAndForgotPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ForgotPasswords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForgotPasswords_CustomerId",
                table: "ForgotPasswords",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForgotPasswords_Customers_CustomerId",
                table: "ForgotPasswords",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForgotPasswords_Customers_CustomerId",
                table: "ForgotPasswords");

            migrationBuilder.DropIndex(
                name: "IX_ForgotPasswords_CustomerId",
                table: "ForgotPasswords");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ForgotPasswords");
        }
    }
}
