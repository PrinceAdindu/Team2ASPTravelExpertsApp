﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelExpertsData.Migrations
{
    /// <inheritdoc />
    public partial class ProfImageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "Customers",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Customers");
        }
    }
}
