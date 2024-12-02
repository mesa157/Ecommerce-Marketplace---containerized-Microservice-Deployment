using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAndRatingService.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 9, 44, 13, 115, DateTimeKind.Utc).AddTicks(88));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 9, 44, 13, 115, DateTimeKind.Utc).AddTicks(337));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 9, 40, 27, 619, DateTimeKind.Utc).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 9, 40, 27, 619, DateTimeKind.Utc).AddTicks(3613));
        }
    }
}
