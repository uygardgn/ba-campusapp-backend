using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BACampusApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityIdAndDummyRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e6e414a-0582-4c97-afb9-c164d28d044a", null, "Student", "STUDENT" },
                    { "7a9acecf-b8c2-4c3f-8b59-42641f962884", null, "Trainer", "TRAINER" },
                    { "957e9abe-fa2f-46d1-9951-b0918f321b85", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e6e414a-0582-4c97-afb9-c164d28d044a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a9acecf-b8c2-4c3f-8b59-42641f962884");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "957e9abe-fa2f-46d1-9951-b0918f321b85");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Admins");
        }
    }
}
