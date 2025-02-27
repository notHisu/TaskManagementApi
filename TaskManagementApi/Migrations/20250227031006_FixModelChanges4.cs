using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class FixModelChanges4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskComments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskComments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskComments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskLabels",
                keyColumns: new[] { "LabelId", "TaskId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TaskLabels",
                keyColumns: new[] { "LabelId", "TaskId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "TaskLabels",
                keyColumns: new[] { "LabelId", "TaskId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "TaskLabels",
                keyColumns: new[] { "LabelId", "TaskId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "TaskLabels",
                keyColumns: new[] { "LabelId", "TaskId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskComments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskItems",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskComments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "04400ed9-a446-415c-afaf-c0214b8942a8", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEJJMSTNDAI75S0MAHOYFpK5VJqZuDwiOYZIKXRHmq+KJWYHsa0WVm06nb0HMHHrYEg==", null, false, "STATIC-SECURITY-STAMP-ADMIN", false, "admin" });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsCompleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 2, 27, 2, 59, 39, 601, DateTimeKind.Utc).AddTicks(4454), "Study the basics of ASP.NET Core framework and its components", false, "Learn ASP.NET Core", "1" },
                    { 2, 2, new DateTime(2025, 2, 27, 2, 59, 39, 601, DateTimeKind.Utc).AddTicks(5177), "Set up a new ASP.NET Core project using Visual Studio or Visual Studio Code", false, "Create a new project", "1" },
                    { 3, 2, new DateTime(2025, 2, 27, 2, 59, 39, 601, DateTimeKind.Utc).AddTicks(5179), "Implement a new feature based on project requirements", false, "Add a new feature", "1" },
                    { 4, 2, new DateTime(2025, 2, 27, 2, 59, 39, 601, DateTimeKind.Utc).AddTicks(5180), "Deploy the application to a hosting service like Azure or AWS", false, "Deploy the app", "1" }
                });

            migrationBuilder.InsertData(
                table: "TaskComments",
                columns: new[] { "Id", "Content", "CreatedAt", "TaskId", "UserId" },
                values: new object[,]
                {
                    { 1, "This is a great task!", new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "1" },
                    { 2, "I'm making progress on this task.", new DateTime(2021, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "1" },
                    { 3, "I'm excited to start this project.", new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "1" }
                });

            migrationBuilder.InsertData(
                table: "TaskLabels",
                columns: new[] { "LabelId", "TaskId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 }
                });
        }
    }
}
