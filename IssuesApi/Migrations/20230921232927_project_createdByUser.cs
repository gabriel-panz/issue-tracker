using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssuesApi.Migrations
{
    /// <inheritdoc />
    public partial class project_createdByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(4778),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 317, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(1342),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 316, DateTimeKind.Local).AddTicks(1096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 850, DateTimeKind.Local).AddTicks(7009),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 315, DateTimeKind.Local).AddTicks(7684));

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Project",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 849, DateTimeKind.Local).AddTicks(6343),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 314, DateTimeKind.Local).AddTicks(6922));

            migrationBuilder.CreateIndex(
                name: "IX_Project_CreatedByUserId",
                table: "Project",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_CreatedByUserId",
                table: "Project",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_CreatedByUserId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_CreatedByUserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Project");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 317, DateTimeKind.Local).AddTicks(1759),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(4778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 316, DateTimeKind.Local).AddTicks(1096),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(1342));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 315, DateTimeKind.Local).AddTicks(7684),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 850, DateTimeKind.Local).AddTicks(7009));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 21, 3, 19, 314, DateTimeKind.Local).AddTicks(6922),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 849, DateTimeKind.Local).AddTicks(6343));
        }
    }
}
