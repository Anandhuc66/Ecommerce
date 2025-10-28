using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Entity.Migrations
{
    /// <inheritdoc />
    public partial class Makenewfieldforregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SuppliersSet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuppliersSet_UserId",
                table: "SuppliersSet",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SuppliersSet_AspNetUsers_UserId",
                table: "SuppliersSet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuppliersSet_AspNetUsers_UserId",
                table: "SuppliersSet");

            migrationBuilder.DropIndex(
                name: "IX_SuppliersSet_UserId",
                table: "SuppliersSet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SuppliersSet");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "AspNetUsers");
        }
    }
}
