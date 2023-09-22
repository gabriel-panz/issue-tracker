using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssuesApi.Migrations
{
    /// <inheritdoc />
    public partial class tags_createdByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 891, DateTimeKind.Local).AddTicks(2944),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(4778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 890, DateTimeKind.Local).AddTicks(6560),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(1342));

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Tag",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 889, DateTimeKind.Local).AddTicks(7681),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 850, DateTimeKind.Local).AddTicks(7009));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 887, DateTimeKind.Local).AddTicks(7900),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 849, DateTimeKind.Local).AddTicks(6343));

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CreatedByUserId",
                table: "Tag",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_CreatedByUserId",
                table: "Tag",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_CreatedByUserId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_CreatedByUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tag");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(4778),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 891, DateTimeKind.Local).AddTicks(2944));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 851, DateTimeKind.Local).AddTicks(1342),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 890, DateTimeKind.Local).AddTicks(6560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 850, DateTimeKind.Local).AddTicks(7009),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 889, DateTimeKind.Local).AddTicks(7681));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 21, 20, 29, 27, 849, DateTimeKind.Local).AddTicks(6343),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 9, 22, 10, 44, 10, 887, DateTimeKind.Local).AddTicks(7900));
        }
    }
}
