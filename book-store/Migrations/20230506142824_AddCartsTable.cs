using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_store.Migrations
{
    /// <inheritdoc />
    public partial class AddCartsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CartModel_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_CartModel_CartModelId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartModel",
                table: "CartModel");

            migrationBuilder.RenameTable(
                name: "CartModel",
                newName: "Carts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Carts_CartModelId",
                table: "Books",
                column: "CartModelId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Carts_CartModelId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "CartModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartModel",
                table: "CartModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CartModel_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "CartModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_CartModel_CartModelId",
                table: "Books",
                column: "CartModelId",
                principalTable: "CartModel",
                principalColumn: "Id");
        }
    }
}
