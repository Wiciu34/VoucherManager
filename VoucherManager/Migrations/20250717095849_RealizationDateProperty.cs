using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoucherManager.Migrations
{
    /// <inheritdoc />
    public partial class RealizationDateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RealizationDate",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealizationDate",
                table: "Vouchers");
        }
    }
}
