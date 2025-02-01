﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingBasket.Migrations
{
    /// <inheritdoc />
    public partial class BasketStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ShoppingBaskets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ShoppingBaskets");
        }
    }
}
