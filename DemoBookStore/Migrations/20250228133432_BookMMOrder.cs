using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookStore.Migrations
{
    /// <inheritdoc />
    public partial class BookMMOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_OrderModel_OrderModelId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_AspNetUsers_UserId",
                table: "OrderModel");

            migrationBuilder.DropIndex(
                name: "IX_Books_OrderModelId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "BookModelOrderModel",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModelOrderModel", x => new { x.BooksId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_BookModelOrderModel_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookModelOrderModel_OrderModel_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "OrderModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModelOrderModel_OrdersId",
                table: "BookModelOrderModel",
                column: "OrdersId");

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

            migrationBuilder.DropTable(
                name: "BookModelOrderModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_OrderModelId",
                table: "Books",
                column: "OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_OrderModel_OrderModelId",
                table: "Books",
                column: "OrderModelId",
                principalTable: "OrderModel",
                principalColumn: "Id");

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
