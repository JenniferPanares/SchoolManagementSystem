using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAQOJLg7B3LRkOSEbL2UOjY64k7zXdBOln3nWv056QdBTbe9nyJNn5aHUqm0lO84rQ==");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDRIEsfmZ8KLTcpYVDDTYPeKz9kliGSJ8X956OvrGbO9oDhH+6YCZUWt+d2O/O3Dpg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPJIaKsle6b8QE8mfGhMgrAC6pyNZst6wQUa2yKGR+lXpjnLpjIwbLp7gxtS7nCUJg==");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOsKzDIu5R66G83c/tk8ckHxfAUM2biZm4GI6co15tiQR88tVGlt3nEogaTResZwOw==");
        }
    }
}
