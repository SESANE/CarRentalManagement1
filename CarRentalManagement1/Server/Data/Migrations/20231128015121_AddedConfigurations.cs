using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalManagement1.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad2bcf0c-20db-474f-8407-5a6b159518ba", null, "Administrator", "ADMINISTRATOR" },
                    { "bd2bcf0c-20db-474f-8407-5a6b159518bb", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3781efa7-66dc-47f0-860f-e506d04102e4", 0, "cf18d1db-4f53-4c32-b164-ab54639486fd", "admin@localhost.com", false, "Admin", "User", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEBg2mGJ+25409lYVAl2qMjW+RjxOPPh66cxFAT9D0KOCrJaQ6Qoig2kt1Wtt4/NsHQ==", null, false, "2b663edf-db41-4fce-a80a-10d72a78eeee", false, "admin@localhost.com" });

            migrationBuilder.UpdateData(
                table: "Colours",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(5376), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(5415) });

            migrationBuilder.UpdateData(
                table: "Colours",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(5421), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(5422) });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6255), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6256), "BMW", "System" },
                    { 2, "System", new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6261), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6262), "Toyota", "System" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6596), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6597), "3 Series", "System" },
                    { 2, "System", new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6602), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6603), "X5", "System" },
                    { 3, "System", new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6604), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6605), "Prius", "System" },
                    { 4, "System", new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6606), new DateTime(2023, 11, 28, 9, 51, 21, 549, DateTimeKind.Local).AddTicks(6607), "Rav4", "System" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad2bcf0c-20db-474f-8407-5a6b159518ba", "3781efa7-66dc-47f0-860f-e506d04102e4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2bcf0c-20db-474f-8407-5a6b159518bb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad2bcf0c-20db-474f-8407-5a6b159518ba", "3781efa7-66dc-47f0-860f-e506d04102e4" });

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad2bcf0c-20db-474f-8407-5a6b159518ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3781efa7-66dc-47f0-860f-e506d04102e4");

            migrationBuilder.UpdateData(
                table: "Colours",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9135), new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9159) });

            migrationBuilder.UpdateData(
                table: "Colours",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9162), new DateTime(2023, 11, 28, 9, 43, 50, 103, DateTimeKind.Local).AddTicks(9163) });
        }
    }
}
