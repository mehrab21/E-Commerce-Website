using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class updatedproductandcategorymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_Cat_Id",
                table: "tbl_Product",
                column: "Cat_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Product_tbl_Category_Cat_Id",
                table: "tbl_Product",
                column: "Cat_Id",
                principalTable: "tbl_Category",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Product_tbl_Category_Cat_Id",
                table: "tbl_Product");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Product_Cat_Id",
                table: "tbl_Product");
        }
    }
}
