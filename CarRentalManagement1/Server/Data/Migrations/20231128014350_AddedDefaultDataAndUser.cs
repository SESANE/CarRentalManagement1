using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalManagement1.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultDataAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Colours",
                columns: new[] { "id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9135), new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9159), "Black", "System" },
                    { 2, "System", new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9162), new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9163), "Blue", "System" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colours",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Colours",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
