using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssuesApi.Migrations
{
    /// <inheritdoc />
    public partial class uniqueLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 317, DateTimeKind.Local).AddTicks(1759),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 528, DateTimeKind.Local).AddTicks(3436));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 316, DateTimeKind.Local).AddTicks(1096),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 527, DateTimeKind.Local).AddTicks(3067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 315, DateTimeKind.Local).AddTicks(7684),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 526, DateTimeKind.Local).AddTicks(9423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 314, DateTimeKind.Local).AddTicks(6922),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 525, DateTimeKind.Local).AddTicks(9413));

            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Login",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 528, DateTimeKind.Local).AddTicks(3436),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 317, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 527, DateTimeKind.Local).AddTicks(3067),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 316, DateTimeKind.Local).AddTicks(1096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 526, DateTimeKind.Local).AddTicks(9423),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 315, DateTimeKind.Local).AddTicks(7684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 20, 52, 27, 525, DateTimeKind.Local).AddTicks(9413),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 314, DateTimeKind.Local).AddTicks(6922));
        }
    }
}
