using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoucherManager.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SellDate",
                table: "Vouchers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationDate",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Vouchers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_GuestId",
                table: "Vouchers",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Guest_GuestId",
                table: "Vouchers",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Guest_GuestId",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_GuestId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ActivationDate",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Vouchers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SellDate",
                table: "Vouchers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
