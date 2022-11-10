using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Data.Migrations
{
    public partial class ModifyNameProductGalleries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGallerys_Products_ProductId",
                table: "ProductGallerys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGallerys",
                table: "ProductGallerys");

            migrationBuilder.RenameTable(
                name: "ProductGallerys",
                newName: "ProductGalleries");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGallerys_ProductId",
                table: "ProductGalleries",
                newName: "IX_ProductGalleries_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGalleries",
                table: "ProductGalleries",
                column: "ProductGalleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGalleries",
                table: "ProductGalleries");

            migrationBuilder.RenameTable(
                name: "ProductGalleries",
                newName: "ProductGallerys");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGallerys",
                newName: "IX_ProductGallerys_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGallerys",
                table: "ProductGallerys",
                column: "ProductGalleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGallerys_Products_ProductId",
                table: "ProductGallerys",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
