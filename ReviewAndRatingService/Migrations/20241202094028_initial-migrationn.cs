using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAndRatingService.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 9, 39, 32, 859, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 9, 39, 32, 859, DateTimeKind.Utc).AddTicks(1448));
        }
    }
}
