using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookStore.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_AspNetUsers_UserId",
                table: "OrderModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_AspNetUsers_UserId",
                table: "OrderModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_AspNetUsers_UserId",
                table: "OrderModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_AspNetUsers_UserId",
                table: "OrderModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
