using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderUpdateRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrackingNumber",
                table: "Shippings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Carrier",
                table: "Shippings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderUpdateRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerNameUpdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddressUpdate_Country = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ShippingAddressUpdate_City = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ShippingAddressUpdate_PostalCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    ShippingAddressUpdate_StreetAddress = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderUpdateRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderUpdateRequests_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderUpdateRequests_OrderId",
                table: "OrderUpdateRequests",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderUpdateRequests");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingNumber",
                table: "Shippings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Carrier",
                table: "Shippings",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
